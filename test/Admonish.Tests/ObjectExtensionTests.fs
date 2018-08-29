module ObjectExtensionTests

open Expecto
open Admonish
open System

[<Tests>]
let nonNull =
  testList "Object.NonNull" [
    vtest "Should add an error for null" <| fun vr ->
      let n : Nullable<int> = Nullable()
      vr
        .NonNull("obj", null)
        .NonNull("int?", n) |> ignore

      expectKeys vr ["obj"; "int?"]

    vtest "Should not add an error for non-null" <| fun vr ->
      let n : Nullable<int> = Nullable(1)
      vr
        .NonNull("key", new obj())
        .NonNull("int?", n) |> ignore

      expectSuccess vr
  ]