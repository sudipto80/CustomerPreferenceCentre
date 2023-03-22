module ChoiceParser

open System.Text.RegularExpressions
open PrefTypes
open System
open System.IO




let toFullString (x: string) =
    match x.ToUpper() with
    | "SUN" -> "Sunday"
    | "MON" -> "Monday"
    | "TUE" -> "Tuesday"
    | "WED" -> "Wednesday"
    | "THU" -> "Thursday"
    | "FRI" -> "Friday"
    | "SAT" -> "Saturday"
    | _ -> raise (new System.ArgumentException($"Day name {x} is not recognized."))


let toUserChoice (strRep:string) =
    match strRep.ToLower() with
    | "never" -> Some(Never)
    | "everyday" -> Some(Everyday)
    | whatever ->
        try
            if Regex.Match(whatever, "[A-Za-z, ]+").Success then
                Some(DaysOfWeek(whatever.Split(',') |> Array.map toFullString))
            else
                Some(Day(Convert.ToInt32(whatever) - 1))
        with
        | :? System.FormatException ->
            printfn "The format for the day is wrong"
            printfn $"{strRep} is not a valid integer."
            printfn "Please choose a number between 1 and 28"
            None
        

let getUserChoices (location: string) =
    File.ReadAllLines(location)
    |> Array.map (fun line -> line.Split(':'))
    |> Array.map (fun toks -> (toks.[0], toUserChoice toks.[1]))
