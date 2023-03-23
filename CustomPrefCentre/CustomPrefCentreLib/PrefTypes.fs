namespace CustomPrefCentreLib

/// Types in the system
module PrefTypes =
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
        { DateString: string
          Index: string
          DayOfTheWeek: System.DayOfWeek }
