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

/// Checks that the specified `ValidationResult` contains the specified error message.
let expectErrorMessage (vr: ValidationResult) (expectedMessage: string) =
  Expect.isFalse vr.Success "The result was expected to be failed, but was successful"
  let d = vr.ToDictionary()
  Expect.equal d.Count 1 "The result was expected to contain exactly one key"
  let vs = (Seq.head d).Value
  Expect.equal vs.Length 1 "The result was expected to contain exactly one message"
  Expect.equal vs.[0] expectedMessage "The error message was incorrect"

/// Checks that the specified `ValidationResult` is a success.
let expectSuccess (vr: ValidationResult) =
    Expect.isTrue vr.Success <| sprintf "Failed result was unexpected. Errors:\n%O" vr