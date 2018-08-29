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

      let actualKeys = vr.ToDictionary().Keys
      Expect.containsAll actualKeys ["null"; "empty"] "Unexpected keys"

    vtest "Should not add an error for a correct string" <| fun vr ->
      vr
        .NonNullOrEmpty("key", "valid") |> ignore

      Expect.isTrue vr.Success "Failed result was unexpected"
  ]

[<Tests>]
let nonNullOrWhiteSpace =
  testList "String.NonNullOrWhiteSpace" [
    vtest "Should add an error for an incorrect string" <| fun vr ->
      vr
        .NonNullOrWhiteSpace("whitespace", " ")
        .NonNullOrWhiteSpace("null", null)
        .NonNullOrWhiteSpace("empty", "") |> ignore

      let actualKeys = vr.ToDictionary().Keys
      Expect.containsAll actualKeys ["whitespace"; "null"; "empty"] "Unexpected keys"

    vtest "Should not add an error for a correct string" <| fun vr ->
      vr
        .NonNullOrEmpty("key", "valid") |> ignore

      Expect.isTrue vr.Success "Failed result was unexpected"
  ]

[<Tests>]
let matches =
  let reg = Regex("a.")
  testList "String.Matches" [
    vtest "Should add an error for an incorrect string" <| fun vr ->
      vr.Matches("null", null, reg)
        .Matches("nonmatcn", "bb", reg) |> ignore

      let actualKeys = vr.ToDictionary().Keys
      Expect.containsAll actualKeys ["null"; "nonmatcn"] "Unexpected keys"

    vtest "Should add the specified error message" <| fun vr ->
      vr.Matches("nonmatcn", "bb", reg, "fail") |> ignore

      let messages = vr.ToDictionary().["nonmatcn"]
      Expect.sequenceEqual messages ["fail"] "Unexpected keys"


    vtest "Should not add an error for a correct string" <| fun vr ->
      vr.Matches("nonmatcn", "aa", reg) |> ignore

      Expect.isTrue vr.Success "Failed result was unexpected"
  ]

[<Tests>]
let lengthBetween =
  testList "String.LengthBetween" [
    vtest "Should add an error for an incorrect string" <| fun vr ->
      vr.LengthBetween("null", null, 3, 4)
        .LengthBetween("short", "12", 3, 4)
        .LengthBetween("long", "12345", 3, 4) |> ignore

      let actualKeys = vr.ToDictionary().Keys
      Expect.containsAll actualKeys ["null"; "short"; "long"] "Unexpected keys"

    vtest "Should not add an error for a correct string" <| fun vr ->
      vr.LengthBetween("length", "123", 3, 4) |> ignore

      Expect.isTrue vr.Success "Failed result was unexpected"
  ]