module ExtensionTests

open Expecto
open Admonish

/// A test of ValidationResult.
let vtest name (body: ValidationResult -> unit) = testCase name <| fun _ ->
  let vr = Validator.Create()
  body vr

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

[<Tests>]
let nonNullOrEmpty =
  testList "NonNullOrEmpty" [
    vtest "Should add an error for an incorrect string" <| fun vr ->
      vr
        .NonNullOrEmpty("null", null)
        .NonNullOrEmpty("empty", "") |> ignore

      let actualKeys = vr.ToDictionary().Keys
      Expect.containsAll actualKeys ["null"; "empty"] "Unexpected keys"

    vtest "Should not add an error for a correct string" <| fun vr ->
      vr
        .NonNullOrEmpty("key", "valid") |> ignore

      Expect.isTrue vr.Success "Failed result was unexpected"
  ]
