open System
open System.Text

printfn "Welcome to Customer Preference Centre"
printfn "Enter the path of the file with customer choices : "
let choiceFilelocation = Console.ReadLine()


printfn "Enter starting year: "
let year = Console.ReadLine() |> int
printfn "Enter starting month: "
let month = Console.ReadLine() |> int
printfn "Enter starting day of the month: "
let day = Console.ReadLine() |> int

let startDate = new DateTime(year, month, day)

printfn "How many days you want to print the report for  ? "
let howManyDays = Console.ReadLine() |> int

printfn "Report"
printfn "----------------------------------------------------"

let stopwatch = new System.Diagnostics.Stopwatch()
stopwatch.Start()
let reportRows =
    CustomPrefCentreLib.Facade.generateReport choiceFilelocation startDate howManyDays
stopwatch.Stop()


let concatUsers names =
    names
    |> Array.sortBy(fun name -> name)
    |> String.concat ","

for (day, users) in reportRows do
    let dayName = day.ToString("dd-MMMM-yyyy")
    printfn $"{(day.DayOfWeek.ToString().Substring(0, 3))} {dayName} {(concatUsers users)}"

printfn "----------------------------------------------------"
printfn $"Time taken {stopwatch.ElapsedMilliseconds} ms"
printfn "----------------------------------------------------"