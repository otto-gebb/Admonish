module NumericExtensionTests

open Expecto
open Admonish

[<Tests>]
let minTests =
  testList "Min" [
    vtest "Should add an error for a too small value" <| fun vr ->
      vr
        .Min("int", 0, 1)
        .Min("decimal", 0m, 1m) |> ignore

      expectKeys vr ["int"; "decimal"]

    vtest "Should not add an error for a value on the boundary" <| fun vr ->
      vr
        .Min("int", 1, 1)
        .Min("decimal", 1m, 1m) |> ignore

      expectSuccess vr

    vtest "Should not add an error for a big enough value" <| fun vr ->
      vr
        .Min("int", 2, 1)
        .Min("decimal", 2m, 1m) |> ignore

      expectSuccess vr
  ]

[<Tests>]
let maxTests =
  testList "Max" [
    vtest "Should add an error for a too big value" <| fun vr ->
      vr
        .Max("int", 2, 1)
        .Max("decimal", 2m, 1m) |> ignore

      expectKeys vr ["int"; "decimal"]

    vtest "Should not add an error for a value on the boundary" <| fun vr ->
      vr
        .Max("int", 1, 1)
        .Max("decimal", 1m, 1m) |> ignore

      expectSuccess vr

    vtest "Should not add an error for a small enough value" <| fun vr ->
      vr
        .Max("int", 0, 1)
        .Max("decimal", 0m, 1m) |> ignore

      expectSuccess vr
  ]

[<Tests>]
let betweenTests =
  testList "Between" [
    vtest "Should add an error for a too big value" <| fun vr ->
      vr
        .Between("int", 11, 1, 10)
        .Between("decimal", 11m, 1m, 10m) |> ignore

      expectKeys vr ["int"; "decimal"]

    vtest "Should add an error for a too small value" <| fun vr ->
      vr
        .Between("int", 0, 1, 10)
        .Between("decimal", 0m, 1m, 10m) |> ignore

      expectKeys vr ["int"; "decimal"]

    vtest "Should not add an error for a value on the boundary" <| fun vr ->
      vr
        .Between("int", 1, 1, 10)
        .Between("int", 10, 1, 10)
        .Between("decimal", 1m, 1m, 10m)
        .Between("decimal", 10m, 1m, 10m)|> ignore

      expectSuccess vr

    vtest "Should not add an error for a value in the range" <| fun vr ->
      vr
        .Between("int", 5, 1, 10)
        .Between("decimal", 5m, 1m, 10m) |> ignore

      expectSuccess vr
  ]