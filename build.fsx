#r "paket: groupref FakeBuild //"
#load "./.fake/build.fsx/intellisense.fsx"
#if !FAKE
#r "netstandard"
#endif

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

Environment.CurrentDirectory <- __SOURCE_DIRECTORY__
let configuration = Environment.environVarOrDefault "Configuration" "Release"
let product = "Admonish"
let description = "Simple validation library for app services and domain entities"
let tags = "validation domain entity app-service"
let authors = "Otto Gebb and contributors"
let owners = "Otto Gebb"

let gitOwner = "otto-gebb"
let gitHome = "https://github.com/" + gitOwner

let projectUrl = sprintf "https://github.com/%s/%s" gitOwner product
let licenceUrl = sprintf "https://github.com/%s/%s/blob/master/LICENSE" gitOwner product
let copyright = "Copyright 2018"

let srcProjects = !! "src/**/*.*proj"
let testProjects = !! "test/**/*Tests.*proj"

// Read additional information from the release notes document
let release = ReleaseNotes.load "RELEASE_NOTES.md"

let getProjectInfo projFilePath =
    let projectName = Path.GetFileNameWithoutExtension projFilePath
    let dir = Path.GetDirectoryName projFilePath
    (projectName, dir)

Target.create "Clean" (fun _ ->
    !! "src/**/bin"
    ++ "src/**/obj"
    ++ "test/**/bin"
    ++ "test/**/obj"
    ++ "nugetpkg"
    |> Shell.cleanDirs 
)

let build project =
    DotNet.build (fun p ->
    { p with
        Configuration = DotNet.BuildConfiguration.Custom configuration
        Common = p.Common
                 |> DotNet.Options.withCustomParams (Some "--no-dependencies")
    }) project

Target.create "Build" (fun _ ->
    srcProjects
    |> Seq.iter build
)


Target.create "ProjectVersion" (fun _ ->
    let setProjectVersion project =
        Xml.pokeInnerText project
            "Project/PropertyGroup/Version" release.NugetVersion
    srcProjects
    |> Seq.iter setProjectVersion
)

Target.create "BuildTest" (fun _ ->
    testProjects
    |> Seq.iter build
)

Target.create "RunTest" (fun _ ->
    let runTest project =
        let (projName, projDir) = getProjectInfo project
        let dll = projDir </> "bin" </> configuration </> "netcoreapp2.1" </> projName + ".dll"
        DotNet.exec id dll ""
        |> fun r -> if r.ExitCode<>0 then projName+".dll failed" |> failwith
    testProjects
    |> Seq.iter runTest
)

Target.create "BuildPackage" (fun _ ->
  let pack project =
    let (projName, _) = getProjectInfo project
    let packParameters =
      [
        sprintf "-c %s" configuration
        sprintf "-o \"%s\"" "../../nugetpkg"
        "--no-build"
        "--no-restore"
        sprintf "/p:Title=\"%s\"" projName
        "/p:PackageVersion=" + release.NugetVersion
        sprintf "/p:Authors=\"%s\"" authors
        sprintf "/p:Owners=\"%s\"" owners
        "/p:PackageRequireLicenseAcceptance=false"
        sprintf "/p:Description=\"%s\"" description
        sprintf "/p:PackageReleaseNotes=\"%O\""
          ((String.toLines release.Notes).Replace(",",""))
        sprintf "/p:Copyright=\"%s\"" copyright
        sprintf "/p:PackageTags=\"%s\"" tags
        sprintf "/p:PackageProjectUrl=\"%s\"" projectUrl
        //sprintf "/p:PackageIconUrl=\"%s\"" iconUrl
        sprintf "/p:PackageLicenseUrl=\"%s\"" licenceUrl
      ] |> String.concat " "
    "pack " + project + " " + packParameters
    |> DotNet.exec id <| ""
    |> ignore
  !! ("src/**/" + product + ".*proj")
  |> Seq.iter pack
)

Target.create "PublishNuget" (fun _ ->
    Paket.push (fun p -> { p with WorkingDir = "nugetpkg" })
)

Target.create "Release" (fun _ ->
    // To run this target create a file named "release.cmd" with the following contents:
    // SET GITHUB_TOKEN=<your_github_token>
    // fake.cmd Release

    let gitName = product
    let v = release.NugetVersion
    let isPreRelease = release.SemVer.PreRelease.IsSome

    Git.Staging.stageAll ""
    Git.Commit.exec "" (sprintf "Bump version to %O" v)
    Git.Branches.push ""

    Git.Branches.tag "" v
    Git.Branches.pushTag "" "origin" v

    Environment.environVar "GITHUB_TOKEN"
    |> GitHub.createClientWithToken
    |> GitHub.draftNewRelease gitOwner gitName v isPreRelease release.Notes
    |> GitHub.publishDraft
    |> Async.RunSynchronously
)

Target.create "All" ignore

"Clean"
  ==> "ProjectVersion"
  ==> "Build"
  ==> "BuildTest"
  ==> "RunTest"
  ==> "All"
  ==> "BuildPackage"
//  AppVeyor will run this.
  ==> "PublishNuget"

"BuildPackage"
//  ==> "PublishNuget" AppVeyor will publish on tag.
  ==> "Release"

Target.runOrDefault "All"
