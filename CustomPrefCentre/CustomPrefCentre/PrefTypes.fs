module PrefTypes

type UserChoice =
    | Never
    | Everyday
    | Day of int
    | DaysOfWeek of string array

type Day =
    { DateString: string
      Index: string
      DayOfTheWeek: string }

let Weekdays =
    [| "Sunday"
       "Monday"
       "Tuesday"
       "Wednesday"
       "Thursday"
       "Friday"
       "Saturday" |]
