[<AutoOpen>]
module Utils

open Admonish
open Expecto

/// A test of ValidationResult.
let vtest name (body: ValidationResult -> unit) = testCase name <| fun _ ->
  let vr = Validator.Create()
  body vr

/// Checks that the specified `ValidationResult` contains
/// the specified keys.
let expectKeys (vr: ValidationResult) (expectedKeys: seq<string>) =
  let actualSorted = vr.ToDictionary().Keys |> Seq.sort
  let expectedSorted = Seq.sort expectedKeys
  Expect.sequenceEqual actualSorted expectedSorted "Unexpected keys"

/// Checks that the specified `ValidationResult` is a success.
let expectSuccess (vr: ValidationResult) =
    Expect.isTrue vr.Success <| sprintf "Failed result was unexpected. Errors:\n%O" vr