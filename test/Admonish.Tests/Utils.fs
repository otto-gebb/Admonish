[<AutoOpen>]
module Utils

open Admonish
open Expecto

/// A test of ValidationResult.
let vtest name (body: ValidationResult -> unit) = testCase name <| fun _ ->
  let vr = Validator.Create()
  body vr
