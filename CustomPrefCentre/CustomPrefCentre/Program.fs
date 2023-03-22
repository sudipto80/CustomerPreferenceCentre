open System

printfn "Welcome to Customer Preference Centre"
printfn "Enter the path of the file with customer choices : "
let choiceFilelocation = Console.ReadLine()


printfn "Enter starting year: ";
let year = Console.ReadLine() |> int 
printfn "Enter starting month: ";
let month = Console.ReadLine() |> int;
printfn "Enter starting day of the month: "
let day = Console.ReadLine() |> int 

let startDate = new DateTime(year,month,day);

printfn "How many days you want to print the report for  ? "
let howManyDays = Console.ReadLine() |> int 

printfn "Report"
printfn "----------------------------------------------------"
let results = CustomPrefCentreLib.Facade.solve choiceFilelocation startDate howManyDays

for (day, users) in results do
    let date = DateTime.Parse(day)
    let dow = date.DayOfWeek.ToString()
    printfn "%A %A %A" (date.ToShortDateString()) dow  users

