namespace CustomPrefCentreLib

/// Holds functionalities for the core logic for generating the report
module Logic =
    open PrefTypes
    open System

    let Weekdays =
        [| DayOfWeek.Sunday
           DayOfWeek.Monday
           DayOfWeek.Tuesday
           DayOfWeek.Wednesday
           DayOfWeek.Thursday
           DayOfWeek.Friday
           DayOfWeek.Saturday|]

    /// <summary>
    /// Returns a flattened list of user choices.
    /// For example if the choice is Tuesday and Friday, and user name is "C" then it returns
    /// [("Tuesday","C");["Friday","C"]
    /// </summary>
    /// <param name="user">User name</param>
    /// <param name="choice">User's choice, everyday,never, a given integer frequency or a collection of days</param>
    let dayIndexedChoices (user: string) (choice: UserChoice) =
        //Range is hardcoded as 90 days
        match choice with
        | Day onThisDay ->
            let theseDays =
                [| onThisDay..90 |]
                |> Array.filter (fun t -> t % onThisDay = 0)
                |> Array.map (fun t -> (string t, user))

            Some(theseDays)
        | DaysOfWeek days -> Some(days |> Array.map (fun day -> (string day, user)))
        | Everyday -> Some(Weekdays |> Array.map (fun day -> (string day, user)))
        | Never
        | _ -> None

    /// <summary>
    /// Creates an index based on dates and user names
    /// The index is the day, the value is user names
    /// </summary>
    /// <param name="loadedChoices">User choices with their names</param>
    let combineChoiceMap (loadedChoices: (string * UserChoice option) []) =
        loadedChoices
        |> Array.filter (fun (_, choice) -> choice.IsSome)
        |> Array.map (fun (user, choice) -> dayIndexedChoices user choice.Value)
        |> Array.filter (fun days -> days.IsSome)
        |> Array.map (fun days -> days.Value)
        |> Array.concat
        |> Array.groupBy fst
        |> Map.ofSeq

    /// <summary>
    /// Returns the list of user names for a given day.
    /// The day can be a date or an index ("10")
    /// </summary>
    /// <param name="daysMap">The map the stores user names indexed by the days</param>
    /// <param name="day">The day or the index</param>
    let getCustomerNames (daysMap: Map<string, (string * string) []>) (day: string) =
        if Map.containsKey day daysMap then
            daysMap.[day] |> Array.map snd
        else
            [||]
