namespace CustomPrefCentreLib

open System
open Logic
open PrefTypes
open ChoiceParser

module Facade =
    /// <summary>
    /// 
    /// </summary>
    /// <param name="daysRange"></param>
    /// <param name="startDate"></param>
    let private getDaysIndices daysRange (startDate: DateTime) =
        daysRange
        |> List.mapi (fun index days -> (index, startDate.AddDays(float days)))
        |> List.map (fun (index, date) ->
            { Index =  index
              Date = date
              DayOfTheWeek = date.DayOfWeek })
    
    let private getResult (days: Day list) combinedChoiceMap =
        days
        |> List.map (fun t ->
            (t.Date.ToShortDateString(),
             Array.concat [| (getCustomerNames combinedChoiceMap (string t.DayOfTheWeek ))
                             (getCustomerNames combinedChoiceMap (string t.Index)) |]))

    let generateReport (location: string) (startDate: DateTime) (howMany: int) =
        let loadedChoices = getUserChoices location
        let combinations = combineChoiceMap loadedChoices
        let daysRange = [ 0..howMany ]
        let daysIndex = getDaysIndices daysRange startDate
        
        getResult daysIndex combinations
