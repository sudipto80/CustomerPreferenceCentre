open CustomPrefCentreLib.Logic
open CustomPrefCentreLib.ChoiceParser
open CustomPrefCentreLib.Logic
open CustomPrefCentreLib.Facade
open CustomPrefCentreLib.PrefTypes
open Expecto
open System



type UserChoice =
    { UserName: string
      Choice: UserChoice option }

[<Tests>]
let reportGenerationTests =
    testList
        "Sample report test"
        [ test "A wants Everyday, B on every 10th and C on Tue,Fri" {
              let path = "..\..\..\TestData\choices.txt"
              let startDate = new DateTime(2018, 4, 1)
              let howManyDays = 13
              let report = generateReport path startDate howManyDays

              Expect.equal 14 report.Length "Expected 14 rows in the report."
              Expect.equal report.[0].CustomerNames [| "A" |] "Expected A to be present on the report but not found."
              Expect.containsAll [| "A"; "B"; "C" |] report.[10].CustomerNames "Expected to find all A,B and C"

              let A_Gets_Everydays =
                  report
                  |> List.forall (fun row -> row.CustomerNames |> Array.contains "A")

              Expect.isTrue A_Gets_Everydays "Expected A to be found on all days"

              //C will get only on Tuesdays and Fridays
              let C_Gets_Only_On_Tuesdays_And_Fridays =
                  report
                  |> List.filter (fun row ->
                      row.Date.DayOfWeek = DayOfWeek.Tuesday
                      || row.Date.DayOfWeek = DayOfWeek.Friday)
                  |> List.forall (fun row -> row.CustomerNames |> Array.contains "C")

              Expect.isTrue
                  C_Gets_Only_On_Tuesdays_And_Fridays
                  "C is expected to get marketing material only on Tuesdays and Fridays"


              //Validating the whole report
              //One row at a time
              let expectedReport =
                  [ { Date = DateTime.Parse("4/1/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/2/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/3/2018")
                      CustomerNames = [| "A"; "C" |] }
                    { Date = DateTime.Parse("4/4/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/5/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/6/2018")
                      CustomerNames = [| "A"; "C" |] }
                    { Date = DateTime.Parse("4/7/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/8/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/9/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/10/2018")
                      CustomerNames = [| "A"; "C"; "B" |] }
                    { Date = DateTime.Parse("4/11/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/12/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/13/2018")
                      CustomerNames = [| "A"; "C" |] }
                    { Date = DateTime.Parse("4/14/2018")
                      CustomerNames = [| "A" |] } ]

              Expect.equal report expectedReport "Something wrong in the report"


          }

          test "Never means Never" {
              let path = "..\..\..\TestData\choices_never.txt"

              let startDate = new DateTime(2018, 4, 1)
              let howManyDays = 13
              let report = generateReport path startDate howManyDays

              Expect.equal report.Length 14 "Expected 14 rows in the report."

              Expect.isTrue
                  (report
                   |> List.forall (fun row -> row.CustomerNames.Length = 0))
                  "Nothing is expected "
          }

          test "Mixed date choices with spaces should work" {
              let path = "..\..\..\TestData\mixed_case_choices_with_spaces.txt"

              let startDate = new DateTime(2018, 4, 1)
              let howManyDays = 13
              let report = generateReport path startDate howManyDays

              let expectedReport =
                  [ { Date = DateTime.Parse("4/1/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/2/2018")
                      CustomerNames = [| "A"; "B" |] }
                    { Date = DateTime.Parse("4/3/2018")
                      CustomerNames = [| "A"; "B"; "C" |] }
                    { Date = DateTime.Parse("4/4/2018")
                      CustomerNames = [||] }
                    { Date = DateTime.Parse("4/5/2018")
                      CustomerNames = [||] }
                    { Date = DateTime.Parse("4/6/2018")
                      CustomerNames = [||] }
                    { Date = DateTime.Parse("4/7/2018")
                      CustomerNames = [||] }
                    { Date = DateTime.Parse("4/8/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/9/2018")
                      CustomerNames = [| "A"; "B" |] }
                    { Date = DateTime.Parse("4/10/2018")
                      CustomerNames = [| "A"; "B"; "C" |] }
                    { Date = DateTime.Parse("4/11/2018")
                      CustomerNames = [||] }
                    { Date = DateTime.Parse("4/12/2018")
                      CustomerNames = [||] }
                    { Date = DateTime.Parse("4/13/2018")
                      CustomerNames = [||] }
                    { Date = DateTime.Parse("4/14/2018")
                      CustomerNames = [||] } ]

              Expect.equal report.Length 14 "Expected 14 rows in the report."

              Expect.equal report expectedReport "Something wrong in the report"
          }


          test "Duplicate choice days are ignored" {
              let path = "..\..\..\TestData\duplicate_choices.txt"
              let startDate = new DateTime(2018, 4, 1)
              let howManyDays = 13
              let report = generateReport path startDate howManyDays


              //Validating the whole report
              //One row at a time
              let expectedReport =
                  [ { Date = DateTime.Parse("4/1/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/2/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/3/2018")
                      CustomerNames = [| "A"; "C" |] }
                    { Date = DateTime.Parse("4/4/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/5/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/6/2018")
                      CustomerNames = [| "A"; "C" |] }
                    { Date = DateTime.Parse("4/7/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/8/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/9/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/10/2018")
                      CustomerNames = [| "A"; "C"; "B" |] }
                    { Date = DateTime.Parse("4/11/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/12/2018")
                      CustomerNames = [| "A" |] }
                    { Date = DateTime.Parse("4/13/2018")
                      CustomerNames = [| "A"; "C" |] }
                    { Date = DateTime.Parse("4/14/2018")
                      CustomerNames = [| "A" |] } ]

              Expect.equal report expectedReport "Something wrong in the report"
          } ]
    |> testLabel "Report tests"


[<Tests>]
let parsingTests =
    testList
        "Parsing tests"
        [ test "Parsing simple choice file" {
              let path = "..\..\..\TestData\choices.txt"
              let choices = CustomPrefCentreLib.ChoiceParser.getUserChoices path

              Expect.equal choices.[0].UserName "A" "Expected A at the start "
              Expect.equal choices.[0].Choice.Value Everyday "A has chosen Everyday ."

              Expect.equal choices.[1].UserName "B" "Second was B"
              Expect.equal choices.[1].Choice.Value (Day(9)) "B selected every 10 days."

              Expect.equal choices.[2].UserName "C" "C was at the end "

              Expect.equal
                  choices.[2].Choice.Value
                  (DaysOfWeek(
                      [| DayOfWeek.Tuesday
                         DayOfWeek.Friday |]
                  ))
                  "C selected Tue and Fri."
          }

          ]
    |> testLabel "Parsing tests"

[<EntryPoint>]
let main args =
    runTestsWithCLIArgs [] args reportGenerationTests
    |> ignore

    runTestsWithCLIArgs [] args parsingTests
