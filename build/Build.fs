open System
open System.IO
open Fake.Api
open Fake.Core
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.DotNet.Testing
open Fake.Tools

open Helpers

initializeContext()
Target.initEnvironment ()

Environment.CurrentDirectory <- __SOURCE_DIRECTORY__ </> ".."
let configuration =
  DotNet.BuildConfiguration.fromEnvironVarOrDefault "Configuration" DotNet.Release
let product = "Admonish"
let description = "Simple validation library for app services and domain entities"
let tags = "validation domain entity app-service"
let authors = "Otto Gebb and contributors"
let owners = "Otto Gebb"

let gitOwner = "otto-gebb"
let gitHome = "https://github.com/" + gitOwner

let projectUrl = sprintf "https://github.com/%s/%s" gitOwner product
let copyright = "Copyright 2019"

let srcProjects = !! "src/**/*.*proj"
let testProjects = !! "test/**/*Tests.*proj"

let nugetpkg = Environment.CurrentDirectory </> "nugetpkg"

// Read additional information from the release notes document
let release = ReleaseNotes.load "RELEASE_NOTES.md"

let getProjectInfo (projFilePath: string) =
    let projectName = Path.GetFileNameWithoutExtension projFilePath
    let dir = Path.GetDirectoryName projFilePath
    (projectName, dir)

Target.create "Clean" (fun _ ->
    !! "src/**/bin"
    ++ "src/**/obj"
    ++ "test/**/bin"
    ++ "test/**/obj"
    ++ "nugetpkg"
    ++ "temp-docs"
    ++ "doc/_site"
    |> Shell.deleteDirs
)

let build project =
  let (_, projDir) = getProjectInfo project
  run dotnet $"build --no-dependencies -c %A{configuration}" projDir

Target.create "Build" (fun _ ->
    srcProjects
    |> Seq.iter build
)

Target.create "ProjectVersion" (fun _ ->
  let setProjectVersion project =
    Xml.pokeInnerText project
      "Project/PropertyGroup/Version" release.NugetVersion
  srcProjects -- "src/Sample/**/*.*proj"
  |> Seq.iter setProjectVersion
)

Target.create "BuildTest" (fun _ ->
  testProjects
  |> Seq.iter build
)

Target.create "RunTest" (fun _ ->
  let runTest project =
    let (projName, projDir) = getProjectInfo project
    let opts = DotNet.Options.withWorkingDirectory projDir
    DotNet.exec opts "run" (sprintf "--no-build --configuration %A" configuration)
    |> fun r -> if r.ExitCode<>0 then projName+".dll failed" |> failwith
  testProjects
  |> Seq.iter runTest
)

Target.create "BuildPackage" (fun _ ->
  let pack project =
    let (projName, projDir) = getProjectInfo project
    let msbuildProps =
      [
        "Title", projName
        "PackageVersion", release.NugetVersion
        "Authors", authors
        "Owners", owners
        "PackageRequireLicenseAcceptance", "false"
        "Description", description
        "PackageReleaseNotes", ((String.toLines release.Notes).Replace(",",""))
        "Copyright", copyright
        "PackageTags", tags
        "PackageProjectUrl", projectUrl
        // "PackageIconUrl", iconUrl
        "PackageLicenseExpression", "MIT"
      ]
    let msbuildParams =
      [for (name,v) in msbuildProps -> $"-p:{name}=\"{v}\""]
      |> String.concat " "
    run dotnet $"pack -c %A{configuration} -o {nugetpkg} --no-build {msbuildParams}" projDir

  !! ("src/**/" + product + ".*proj")
  |> Seq.iter pack
)

// For manual nuget publishing.
Target.create "PublishNuget" (fun _ ->
  // To run this target create a file named "PublishNuget.cmd" with the following contents:
  // SET nugetkey=<your_nuget_api_key>
  // dotnet run --project ./build/Build.fsproj -t PublishNuget
  let key = Environment.environVarOrFail "nugetkey"
  let push (package: string) =
    let name = Path.GetFileNameWithoutExtension package
    DotNet.exec id "nuget" (sprintf "push %s -s https://www.nuget.org -k %s" package key)
    |> fun r -> if r.ExitCode<>0 then name+" push failed" |> failwith
  !! "nugetpkg/*.nupkg"
  |> Seq.iter push
)

Target.create "PublishDocs" (fun _ ->
  Shell.cleanDir "temp-docs"
  let url = sprintf "https://github.com/%s/%s.git" gitOwner product
  Git.Repository.cloneSingleBranch "" url "gh-pages" "temp-docs"
  Git.Repository.fullClean "temp-docs"

  CreateProcess.fromRawCommand "docfx" []
  |> CreateProcess.withWorkingDirectory "doc"
  |> CreateProcess.withTimeout (TimeSpan.FromMinutes 1.0)
  |> CreateProcess.ensureExitCodeWithMessage "docfx failed."
  |> Proc.run
  |> ignore

  Shell.copyRecursive "doc/_site" "temp-docs" true |> printfn "%A"
  Git.Staging.stageAll "temp-docs"
  Git.Commit.exec "temp-docs" (sprintf "Update generated documentation %s" release.NugetVersion)
  Git.Branches.pushBranch "temp-docs" url "gh-pages"
)

Target.create "Release" (fun _ ->
  // To run this target create a file named "release.cmd" with the following contents:
  // SET GITHUB_TOKEN=<your_github_token>
  // dotnet run --project ./build/Build.fsproj -t Release
  let token = Environment.environVarOrFail "GITHUB_TOKEN"
  let v = release.NugetVersion
  let isPreRelease = release.SemVer.PreRelease.IsSome

  Git.Staging.stageAll ""
  Git.Commit.exec "" (sprintf "Bump version to %O" v)
  Git.Branches.push ""

  Git.Branches.tag "" v
  Git.Branches.pushTag "" "origin" v

  token
  |> GitHub.createClientWithToken
  |> GitHub.draftNewRelease gitOwner product v isPreRelease release.Notes
  |> GitHub.publishDraft
  |> Async.RunSynchronously
)

Target.create "All" ignore

#nowarn "20"

"Build"
  ?=> "BuildTest"
  ==> "RunTest"

"Clean"
  ==> "ProjectVersion"
  ==> "Build"
  ==> "All"
  ==> "BuildPackage"
  ==> "PublishNuget"

"RunTest"
  ==> "All"

"BuildPackage"
//  ==> "PublishNuget" AppVeyor will publish on tag.
  ==> "PublishDocs"
  ==> "Release"

[<EntryPoint>]
let main args = runOrDefault args