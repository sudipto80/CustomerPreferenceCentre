namespace CustomPrefCentreLib

open System
open Logic
open PrefTypes
open ChoiceParser

/// The client facing type
module Facade =
    /// <summary>
    /// Creates the days index from the date range
    /// </summary>
    /// <param name="daysRange">Number of days for which we want to create the index</param>
    /// <param name="startDate">The start date of the index</param>
    let private getDaysIndices daysRange (startDate: DateTime) =
        daysRange
        |> List.mapi (fun index days -> (index, startDate.AddDays(float days)))
        |> List.map (fun (index, date) ->
            { Index = index
              Date = date
              DayOfTheWeek = date.DayOfWeek })

    /// <summary>
    /// The joiner, that concatenates the names of the user on each indexed day
    /// </summary>
    /// <param name="days">Number of days to run the index</param>
    /// <param name="combinedChoiceMap">What is the flattened choices of user indexed by date</param>
    let private getResult (days: Day list) combinedChoiceMap =
        days
        |> List.map (fun t ->
            { Date = t.Date
              CustomerNames =
                Array.concat [| (getCustomerNames combinedChoiceMap (string t.DayOfTheWeek))
                                (getCustomerNames combinedChoiceMap (string t.Index)) |] })

    /// <summary>
    /// The facade method to generate the report
    /// </summary>
    /// <param name="location">Location of the input file</param>
    /// <param name="startDate">Start date</param>
    /// <param name="howMany">Number of days to generate the report for</param>
    let generateReport (location: string) (startDate: DateTime) (howMany: int) =
        let loadedChoices = getUserChoices location
        let combinations = combineChoiceMap loadedChoices
        let daysRange = [ 0..howMany ]
        let daysIndex = getDaysIndices daysRange startDate
        getResult daysIndex combinations
