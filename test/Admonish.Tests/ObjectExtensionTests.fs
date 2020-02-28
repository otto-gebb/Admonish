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

    vtest "reference type: Should override the message" <| fun vr ->
      vr.NonNull("obj", null, "msg") |> ignore

      expectErrorMessage vr "msg"

    vtest "nullable type: Should override the message" <| fun vr ->
      let n : Nullable<int> = Nullable()
      vr.NonNull("int?", n, "msg") |> ignore

      expectErrorMessage vr "msg"
  ]

[<Tests>]
let _null =
  testList "Object.Null" [
    vtest "Should add an error for non-null" <| fun vr ->
      let n : Nullable<int> = Nullable(1)
      vr
        .Null("obj", new obj())
        .Null("int?", n) |> ignore

      expectKeys vr ["obj"; "int?"]

    vtest "Should not add an error for null" <| fun vr ->
      let n : Nullable<int> = Nullable()
      vr
        .Null("key", null)
        .Null("int?", n) |> ignore

      expectSuccess vr

    vtest "reference type: Should override the message" <| fun vr ->
      vr.Null("obj", new obj(), "msg") |> ignore

      expectErrorMessage vr "msg"

    vtest "nullable type: Should override the message" <| fun vr ->
      let n : Nullable<int> = Nullable(1)
      vr.Null("int?", n, "msg") |> ignore

      expectErrorMessage vr "msg"
  ]

[<Tests>]
let equal =
  testList "Object.Equal" [
    vtest "Should add an error for non-equal" <| fun vr ->
      vr.Equal("obj", "a", "b") |> ignore

      expectKeys vr ["obj"]

    vtest "Should not add an error for equal" <| fun vr ->
      vr.Equal("obj", 1, 1) |> ignore

      expectSuccess vr

    vtest "Should override the message" <| fun vr ->
      vr.Equal("obj", 0, 1, "msg") |> ignore

      expectErrorMessage vr "msg"
  ]

[<Tests>]
let notEqual =
  testList "Object.NotEqual" [
    vtest "Should add an error for equal" <| fun vr ->
      vr.NotEqual("obj", "a", "a") |> ignore

      expectKeys vr ["obj"]

    vtest "Should not add an error for non-equal" <| fun vr ->
      vr.NotEqual("obj", 0, 1) |> ignore

      expectSuccess vr

    vtest "Should override the message" <| fun vr ->
      vr.NotEqual("obj", 0, 0, "msg") |> ignore

      expectErrorMessage vr "msg"
  ]