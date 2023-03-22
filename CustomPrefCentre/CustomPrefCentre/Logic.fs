module Logic

open PrefTypes
open System


let dayIndexedChoices (user: string) (choice: UserChoice) =
    //Range is hardcoded as 90 days
    match choice with
    | Day onThisDay ->
        let theseDays =
            [| onThisDay..90 |]
            |> Array.filter (fun t -> t % onThisDay = 0)
            |> Array.map (fun t -> (t.ToString(), user))

        Some(theseDays)
    | DaysOfWeek days -> Some(days |> Array.map (fun day -> (day, user)))
    | Everyday -> Some(Weekdays |> Array.map (fun day -> (day, user)))
    | Never
    | _ -> None


let combineChoiceMap (loadedChoices : (string * UserChoice option)[])  =
    loadedChoices
    |> Array.filter(fun (_, choice) -> choice.IsSome)
    |> Array.map (fun (user, choice) -> dayIndexedChoices user choice.Value)
    |> Array.filter (fun days -> days.IsSome)
    |> Array.map (fun days -> days.Value)
    |> Array.concat
    |> Array.groupBy fst
    |> Map.ofSeq
   
let getCustomerNames(daysMap: Map<string,(string * string)[]>)(day:string) = 
    if Map.containsKey day daysMap then daysMap.[day] |> Array.map snd 
    else [||]
          