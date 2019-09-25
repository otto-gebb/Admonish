module EnumExtensionTests

open Expecto
open Admonish

type MyEnum =
  | A = 1
  | B = 2

let badValue = enum 3

[<Tests>]
let minTests =
  testList "Enum.IsDefined" [
    vtest "Should add an error for a non-defined value" <| fun vr ->
      vr.IsDefined<MyEnum>("enum", badValue) |> ignore

      expectKeys vr ["enum"]
      let message = vr.ToDictionary().["enum"] |> Array.head
      Expect.stringContains message (nameof MyEnum) "Default message did not contain enum name"

    vtest "Should override the message" <| fun vr ->
      let expected = "custom message"
      vr.IsDefined<MyEnum>("enum", badValue, expected) |> ignore

      let message = vr.ToDictionary().["enum"] |> Array.head
      Expect.equal message expected "Invalid message"
  ]