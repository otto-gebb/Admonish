module ValidationResultTests

open Expecto
open Admonish

[<Tests>]
let tests =
  testList "ValidationResult" [
    vtest "Created is empty" <| fun vr ->
      Expect.isEmpty (vr.ToDictionary()) "New result contained errors"

    vtest "Empty is a success" <| fun vr ->
      Expect.isTrue vr.Success "New result was unsuccessful"

    vtest "Empty does not throw when asked" <| fun vr ->
      vr.ThrowIfInvalid() |> ignore

    vtest "Non-empty throws when asked" <| fun vr ->
      vr.AddError "dummy" |> ignore

      let act () = vr.ThrowIfInvalid() |> ignore

      // TODO: Remove this when Expect.throwsT is fixed.
      Expect.throws act "Did not throw"
      Expect.throwsT<ValidationException> act "Did not throw the expected exception"

    vtest "Is failed after adding an error" <| fun vr ->
      vr.AddError "dummy" |> ignore
      Expect.isFalse vr.Success "Unexpected success"

    vtest "Contains added parameter and message" <| fun vr ->
      let key, message = "param", "message"

      vr.AddError(key, message) |> ignore

      let errors = vr.ToDictionary()
      let actualKey, actualMessages =
        errors |> Seq.head |> fun kv -> kv.Key, kv.Value
      Expect.equal errors.Count 1 "Unexpected number of errors"
      Expect.equal actualKey key "Unexpected key"
      Expect.sequenceEqual actualMessages [message] "Unexpected message"

    vtest "Adding second error for the same key appends to existing collection" <| fun vr ->
      let key, message1 = "param", "message1"
      let message2 = "message2"

      vr
        .AddError(key, message1)
        .AddError(key, message2)
        |> ignore

      let errors = vr.ToDictionary()
      Expect.equal errors.Count 1 "Unexpected number of errors"
      Expect.sequenceEqual errors.[key] [message1; message2] "Unexpected messages"

    vtest "Adding without a key appends with a special key" <| fun vr ->
      let message1 = "message1"
      let message2 = "message2"

      vr
        .AddError(message1)
        .AddError(message2)
        |> ignore

      let errors = vr.ToDictionary()
      let actualMessages = errors.[ValidationResult.NoKey]
      Expect.equal errors.Count 1 "Unexpected number of errors"
      Expect.sequenceEqual actualMessages [message1; message2] "Unexpected messages"
  ]
