namespace CustomPrefCentreLib

/// This type is used to parse user choices from an input text file
module ChoiceParser =

    open System.Text.RegularExpressions
    open PrefTypes
    open System
    open System.IO

    /// <summary>
    /// Gets full names from abbreviations (if used)
    /// Users can use full day names like Sunday or they can use SUN or sun or Sun
    /// </summary>
    /// <param name="x">the day name from the input file</param>
    let toDoW (x: string) =
        match x.Substring(0, 3).ToUpper().Trim() with
        | "SUN" -> DayOfWeek.Sunday
        | "MON" -> DayOfWeek.Monday
        | "TUE" -> DayOfWeek.Tuesday
        | "WED" -> DayOfWeek.Wednesday
        | "THU" -> DayOfWeek.Thursday
        | "FRI" -> DayOfWeek.Friday
        | "SAT" -> DayOfWeek.Saturday
        | _ -> raise (new System.ArgumentException($"Day name {x} is not recognized."))

    /// <summary>
    /// Transforms the string representation of the user choice to
    /// UserChoice type
    /// </summary>
    /// <param name="strRep">String representation of the user's choice. like "Never" or 10 or "Everyday"</param>
    let toUserChoice (strRep: string) =
        match strRep.ToLower() with
        | "never" -> Some(Never)
        | "everyday" -> Some(Everyday)
        | whatever ->
            try
                if Regex.Match(whatever, "[A-Za-z, ]+").Success then
                    Some(
                        DaysOfWeek(
                            whatever.Split(',')
                            // Users might leave extra spaces in between the day names
                            // so we need to trim it before
                            |> Array.map (fun d -> d.Trim())
                            |> Array.distinct
                            |> Array.map toDoW
                        )
                    )
                else
                    Some(Day(int whatever - 1))
            with
            | :? System.FormatException ->
                printfn "The format for the day is wrong"
                printfn $"{strRep} is not a valid integer."
                printfn "Please choose a number between 1 and 28"
                None

    /// <summary>
    /// Generates a pair with the user name and the choice
    /// </summary>
    /// <param name="parts">string representation of the user name and the choice</param>
    let toUserAndChoicePair (parts: string array) = (parts.[0], toUserChoice parts.[1])

    /// <summary>
    /// Loads user choices from the input text file
    /// </summary>
    /// <param name="location">User choice input text file path</param>
    let getUserChoices (location: string) =
        File.ReadAllLines(location)
        |> Array.map (fun line -> line.Split(':'))
        |> Array.map toUserAndChoicePair
