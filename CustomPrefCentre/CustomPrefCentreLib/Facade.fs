namespace CustomPrefCentreLib

open System
open Logic 
open PrefTypes
open ChoiceParser

module Facade =
   
let private getDaysIndices daysRange (startDate: DateTime) =
    daysRange
    |> List.mapi (fun index days -> (index, startDate.AddDays(float days)))
    |> List.map (fun (index, date) ->
        { Index = index.ToString()
          DateString = date.ToShortDateString()
          DayOfTheWeek = date.DayOfWeek.ToString() })

let private getResult (days: Day list) combinedChoiceMap =
    days
    |> List.map (fun t ->
        (t.DateString,
         Array.concat [| (getCustomerNames combinedChoiceMap t.DayOfTheWeek)
                         (getCustomerNames combinedChoiceMap t.Index) |]))

let solve (location: string) (startDate: DateTime) (howMany: int) =
    let loadedChoices = getUserChoices location
    let combinations = combineChoiceMap loadedChoices
    let daysRange = [ 0 .. howMany ]
    let daysIndex = getDaysIndices  daysRange startDate
    getResult daysIndex combinations
    
