namespace CustomPrefCentreLib

/// Types in the system
module PrefTypes =
    open System

    /// <summary>
    /// Represents user's choice
    /// </summary>
    type UserChoice =
        | Never
        | Everyday
        | Day of int
        | DaysOfWeek of System.DayOfWeek array

    /// <summary>
    /// Represents the index type for generating the report
    /// </summary>
    type Day =
        { Date: DateTime
          Index: int
          DayOfTheWeek: System.DayOfWeek }

    /// <summary>
    /// Represents a row in the user choice input file
    /// </summary>
    type UserChoiceRow =
        { UserName: string
          Choice: UserChoice option }

    /// <summary>
    /// Represents a row in the generated report
    /// </summary>
    type ReportRow =
        { Date: DateTime
          CustomerNames: string array }
