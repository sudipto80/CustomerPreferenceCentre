# Customer Preference Centre
Customer Preference Centre example


## Input 
The program accepts user choices in form of an input text file like this

![image](https://user-images.githubusercontent.com/1287634/227129286-f1e7cb8d-0e1e-46b5-bd6c-075bc4c9b0fb.png)

Each row represents choice of a particular user. For example the first row represents the choice of user A; which is Everyday. 
Last row represents the choice of the user C. 

This string representation is parsed to the following type

```fs
    /// <summary>
    /// Represents user's choice
    /// </summary>
    type UserChoice =
        | Never
        | Everyday
        | Day of int
        | DaysOfWeek of System.DayOfWeek array
```
### Specialities
The program is capable of handling slightly corrupted input files with the following cases. 
* When the input has duplicate days in the row 
* When the input has spaces and mixed names (some long, some short) for days 

## Project structure
There are three projects in solution. 

| Project | Purpose 
|---------|--------
| **`CustomPrefCentreLib`** | Holds core functionalities to implement the Customer Preference Centre
| **`CustomPrefCentre`** | F# Console Application to test the given sample case or any other (see sample run below)
| **`CustomPrefCentreLibTests`** | Expecto tests 

## Sample Run 
To run the program, select `CustomPrefCentre` as the start up project. Here is a sample run for the example scenario for 13 days. 

![](https://github.com/sudipto80/CustomerPreferenceCentre/blob/main/cpcDemo.gif)

## Unit tests 
To run the unit tests select `CustomPrefCentreLibTests` as the startup project. 
Expecto is used to create the unit tests. The following screenshot shows tests ran and passed

![image](https://user-images.githubusercontent.com/1287634/227298035-fd8bab6b-9d76-4e75-aab1-222383eb3940.png)




