module NestedValidatorTests

open Expecto
open Admonish

[<Tests>]
let tests =
  testList "NestedValidators" [
    vtest "WithKey prepends outer key to the error added within the scope" <| fun vr ->
      vr.WithKey("p1", fun x -> x.WithKey("p2", fun y -> y.AddError("p3", "dummy"))) |> ignore

      let key = vr.ToDictionary().Keys |> Seq.head
      Expect.equal key "p1.p2.p3" "Unexpected key"

    vtest "Nested errors added without a key are added to the outer key" <| fun vr ->
      vr.WithKey("p1", fun x -> x.AddError("m1").AddError("m2")) |> ignore

      let errors = vr.ToDictionary()
      Expect.sequenceEqual errors.["p1"] ["m1"; "m2"] "Unexpected messages"

    vtest "Collection item errors with a key have an index prepended" <| fun vr ->
      vr.Collection("p", [1;2], fun vr2 x -> vr2.AddError("key", sprintf "msg%d" x)) |> ignore

      let errors = vr.ToDictionary()
      Expect.sequenceEqual errors.["p[0].key"] ["msg1"] "Unexpected messages"
      Expect.sequenceEqual errors.["p[1].key"] ["msg2"] "Unexpected messages"

    vtest "Collection item errors with no key have an index prepended" <| fun vr ->
      vr.Collection("p", [1;2], fun vr2 x -> vr2.AddError(sprintf "msg%d" x)) |> ignore

      let errors = vr.ToDictionary()
      Expect.sequenceEqual errors.["p[0]"] ["msg1"] "Unexpected messages"
      Expect.sequenceEqual errors.["p[1]"] ["msg2"] "Unexpected messages"
  ]
