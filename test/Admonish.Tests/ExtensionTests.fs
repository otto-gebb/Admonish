module ExtensionTests

open Expecto
open Admonish

[<Tests>]
let check =
  testList "Check" [
    vtest "Should add an error when the condition is false" <| fun vr ->
      vr.Check("key", false, "dummy") |> ignore

      let actualKeys = vr.ToDictionary().Keys
      Expect.sequenceEqual actualKeys ["key"] "Unexpected keys"

    vtest "Should not add an error when the condition is true" <| fun vr ->
      vr.Check("key", true, "dummy") |> ignore

      Expect.isTrue vr.Success "Failed result was unexpected"
  ]

