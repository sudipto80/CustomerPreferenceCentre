open CustomPrefCentreLib.Logic
open CustomPrefCentreLib.ChoiceParser
open CustomPrefCentreLib.Logic
open CustomPrefCentreLib.Facade
open Expecto
open System

[<Tests>]
let tests =

    testList
        "Sample report test"
        [ test "A wants Everyday, B on every 10th and C on Tue,Fri" {
              let path = "..\..\..\TestData\choices.txt"
              let startDate = new DateTime(2018, 4, 1)
              let howManyDays = 13
              let result = CustomPrefCentreLib.Facade.generateReport path startDate howManyDays

              Expect.equal 14 result.Length "Expected 14 rows in the report."
              Expect.equal [| "A" |] (snd result.[0]) "Expected A to be present on the report but not found."
              Expect.containsAll [| "A"; "B"; "C" |] (snd result.[10]) "Expected to find all A,B and C"

              let A_Gets_Everydays =
                  result
                  |> List.forall (fun t -> (snd t) |> Array.contains "A")

              Expect.isTrue A_Gets_Everydays "Expected A to be found on all days"

              //C will get only on Tuesdays and Fridays
              let C_getsOnlyOnTuesdays =
                  result
                  |> List.filter (fun (day, users) ->
                      day.DayOfWeek = DayOfWeek.Tuesday
                      || day.DayOfWeek = DayOfWeek.Friday)
                  |> List.forall (fun (_, users) -> users |> Array.contains "C")

              Expect.isTrue C_getsOnlyOnTuesdays "C is expected to get reports only on Tuesdays and Fridays"


              //Validating the whole report
              //One row at a time
              let expected =
                  [ (DateTime.Parse("4/1/2018"), [| "A" |])
                    (DateTime.Parse("4/2/2018"), [| "A" |])
                    (DateTime.Parse("4/3/2018"), [| "A"; "C" |])
                    (DateTime.Parse("4/4/2018"), [| "A" |])
                    (DateTime.Parse("4/5/2018"), [| "A" |])
                    (DateTime.Parse("4/6/2018"), [| "A"; "C" |])
                    (DateTime.Parse("4/7/2018"), [| "A" |])
                    (DateTime.Parse("4/8/2018"), [| "A" |])
                    (DateTime.Parse("4/9/2018"), [| "A" |])
                    (DateTime.Parse("4/10/2018"), [| "A"; "C"; "B"|])
                    (DateTime.Parse("4/11/2018"), [| "A" |])
                    (DateTime.Parse("4/12/2018"), [| "A" |])
                    (DateTime.Parse("4/13/2018"), [| "A"; "C" |])
                    (DateTime.Parse("4/14/2018"), [| "A" |]) ]

              Expect.equal result expected "Something wrong in the report"


          }

          test "Never means Never" {
              let path = "..\..\..\TestData\choices_never.txt"

              let startDate = new DateTime(2018, 4, 1)
              let howManyDays = 13
              let result = CustomPrefCentreLib.Facade.generateReport path startDate howManyDays

              Expect.equal 14 result.Length "Expected 14 rows in the report."

              Expect.isTrue
                  (result
                   |> List.forall (fun t -> (snd t).Length = 0))
                  "Nothing is expected "
          }

          test "Sun Mon Tue" {
              let path = "..\..\..\TestData\Sun_Mon_Tue.txt"

              let startDate = new DateTime(2018, 4, 1)
              let howManyDays = 13
              let result = CustomPrefCentreLib.Facade.generateReport path startDate howManyDays

              Expect.equal 14 result.Length "Expected 14 rows in the report."


          }

          ]





[<EntryPoint>]
let main args = runTestsWithCLIArgs [] args tests
