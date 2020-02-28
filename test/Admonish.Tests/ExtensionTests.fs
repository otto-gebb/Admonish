module ExtensionTests

open Expecto
open Admonish

[<Tests>]
let check =
  testList "Check" [
    vtest "Should add an error when the condition is false" <| fun vr ->
      vr.Check("key", false, "msg") |> ignore

      expectKeys vr ["key"]
      expectErrorMessage vr "msg"

    vtest "Should not add an error when the condition is true" <| fun vr ->
      vr.Check("key", true, "dummy") |> ignore

      expectSuccess vr
  ]
