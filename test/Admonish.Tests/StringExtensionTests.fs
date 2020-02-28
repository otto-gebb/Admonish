module StringExtensionTests

open Expecto
open Admonish
open System.Text.RegularExpressions

[<Tests>]
let nonNullOrEmpty =
  testList "String.NonNullOrEmpty" [
    vtest "Should add an error for an incorrect string" <| fun vr ->
      vr
        .NonNullOrEmpty("null", null)
        .NonNullOrEmpty("empty", "") |> ignore

      expectKeys vr ["null"; "empty"]

    vtest "Should not add an error for a correct string" <| fun vr ->
      vr
        .NonNullOrEmpty("key", "valid") |> ignore

      expectSuccess vr

    vtest "Should override the message" <| fun vr ->
      vr.NonNullOrEmpty("null", null, "msg") |> ignore

      expectErrorMessage vr "msg"
  ]

[<Tests>]
let nonNullOrWhiteSpace =
  testList "String.NonNullOrWhiteSpace" [
    vtest "Should add an error for an incorrect string" <| fun vr ->
      vr
        .NonNullOrWhiteSpace("whitespace", " ")
        .NonNullOrWhiteSpace("null", null)
        .NonNullOrWhiteSpace("empty", "") |> ignore

      expectKeys vr ["whitespace"; "null"; "empty"]

    vtest "Should not add an error for a correct string" <| fun vr ->
      vr.NonNullOrEmpty("key", "valid") |> ignore

      expectSuccess vr

    vtest "Should override the message" <| fun vr ->
      vr.NonNullOrWhiteSpace("empty", "", "msg") |> ignore

      expectErrorMessage vr "msg"
  ]

[<Tests>]
let matches =
  let reg = Regex("a.")
  testList "String.Matches" [
    vtest "Should add an error for an incorrect string" <| fun vr ->
      vr
        .Matches("null", null, reg)
        .Matches("nonmatcn", "bb", reg) |> ignore

      expectKeys vr ["null"; "nonmatcn"]

    vtest "Should add the specified error message" <| fun vr ->
      vr.Matches("nonmatcn", "bb", reg, "fail") |> ignore

      let messages = vr.ToDictionary().["nonmatcn"]
      Expect.sequenceEqual messages ["fail"] "Unexpected keys"


    vtest "Should not add an error for a correct string" <| fun vr ->
      vr.Matches("nonmatcn", "aa", reg) |> ignore

      expectSuccess vr

    vtest "Should override the message" <| fun vr ->
      vr.Matches("null", null, reg, "msg") |> ignore

      expectErrorMessage vr "msg"
  ]

[<Tests>]
let lengthBetween =
  testList "String.LengthBetween" [
    vtest "Should add an error for an incorrect string" <| fun vr ->
      vr
        .LengthBetween("null", null, 3, 4)
        .LengthBetween("short", "12", 3, 4)
        .LengthBetween("long", "12345", 3, 4) |> ignore

      expectKeys vr ["null"; "short"; "long"]

    vtest "Should not add an error for a correct string" <| fun vr ->
      vr.LengthBetween("length", "123", 3, 4) |> ignore

      expectSuccess vr

    vtest "Should override the message" <| fun vr ->
      vr.LengthBetween("null", null, 3, 4, "msg") |> ignore

      expectErrorMessage vr "msg"
  ]