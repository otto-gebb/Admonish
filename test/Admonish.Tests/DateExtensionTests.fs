module DateExtensionTests

open Expecto
open Admonish
open System

let private dt = DateTime.Today
let private dto = DateTimeOffset.UtcNow

/// Adds the specified number of days to the date.
let inline (+.) (d: ^T) (x: int) : ^T = (^T : (member AddDays : float -> ^T) d, float x)

/// Subtracts the specified number of days from the date.
let inline (-.) (d: ^T) (x: int) : ^T = (^T : (member AddDays : float -> ^T) d, float -x)

[<Tests>]
let minTests =
  testList "DateMin" [
    vtest "Should add an error for a too small value" <| fun vr ->
      vr
        .Min("DateTime", dt, dt +. 1)
        .Min("DateTimeOffset", dto, dto +. 1) |> ignore

      expectKeys vr ["DateTime"; "DateTimeOffset"]

    vtest "Should not add an error for a value on the boundary" <| fun vr ->
      vr
        .Min("DateTime", dt, dt)
        .Min("DateTimeOffset", dto, dto) |> ignore

      expectSuccess vr

    vtest "Should not add an error for a big enough value" <| fun vr ->
      vr
        .Min("DateTime", dt, dt -. 1)
        .Min("DateTimeOffset", dto, dto -. 1) |> ignore

      expectSuccess vr

    vtest "Should override the message" <| fun vr ->
      vr.Min("DateTime", dt, dt +. 1, "msg") |> ignore

      expectErrorMessage vr "msg"
  ]

[<Tests>]
let maxTests =
  testList "DateMax" [
    vtest "Should add an error for a too big value" <| fun vr ->
      vr
        .Max("DateTime", dt, dt -. 1)
        .Max("DateTimeOffset", dto, dto -. 1) |> ignore

      expectKeys vr ["DateTime"; "DateTimeOffset"]

    vtest "Should not add an error for a value on the boundary" <| fun vr ->
      vr
        .Max("DateTime", dt, dt)
        .Max("DateTimeOffset", dto, dto) |> ignore

      expectSuccess vr

    vtest "Should not add an error for a small enough value" <| fun vr ->
      vr
        .Max("DateTime", dt, dt +. 1)
        .Max("DateTimeOffset", dto, dto +. 1) |> ignore

      expectSuccess vr

    vtest "Should override the message" <| fun vr ->
      vr.Max("DateTime", dt, dt -. 1, "msg") |> ignore

      expectErrorMessage vr "msg"
  ]

[<Tests>]
let betweenTests =
  testList "DateBetween" [
    vtest "Should add an error for a too big value" <| fun vr ->
      vr
        .Between("DateTime", dt, dt -. 2, dt -. 1)
        .Between("DateTimeOffset", dto, dto -. 2, dto -. 1) |> ignore

      expectKeys vr ["DateTime"; "DateTimeOffset"]

    vtest "Should add an error for a too small value" <| fun vr ->
      vr
        .Between("DateTime", dt, dt +. 1, dt +. 2)
        .Between("DateTimeOffset", dto, dto +. 1, dto +. 2) |> ignore

      expectKeys vr ["DateTime"; "DateTimeOffset"]

    vtest "Should not add an error for a value on the boundary" <| fun vr ->
      vr
        .Between("DateTime", dt, dt, dt +. 1)
        .Between("DateTime", dt, dt -. 1, dt)
        .Between("DateTimeOffset", dto, dto, dto +. 1)
        .Between("DateTimeOffset", dto, dto -. 1, dto)|> ignore

      expectSuccess vr

    vtest "Should not add an error for a value in the range" <| fun vr ->
      vr
        .Between("DateTime", dt, dt -. 1, dt +. 1)
        .Between("DateTimeOffset", dto, dto -. 1, dto +. 1) |> ignore

      expectSuccess vr

    vtest "Should override the message" <| fun vr ->
      vr.Between("DateTime", dt, dt -. 2, dt -. 1, "msg") |> ignore

      expectErrorMessage vr "msg"
  ]