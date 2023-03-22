

open CustomPrefCentreLib.Logic
open CustomPrefCentreLib.ChoiceParser
open CustomPrefCentreLib.Logic
open CustomPrefCentreLib.Facade
open Expecto
open System

[<Tests>]
let tests =
 
  testList "Sample report test" [
      test "A wants Everyday, B on every 10th and C on Tue,Fri" {
        let path = "C:\Users\Admin\Documents\GitHub\CustomerPreferenceCentre\choices.txt"
        let startDate = new DateTime(2018,4,1)
        let howManyDays = 13
        let result = CustomPrefCentreLib.Facade.solve path startDate howManyDays

        Expect.equal 14 result.Length "Expected 14 rows in the report."
        Expect.equal [|"A"|] (snd result.[0]) "Expected A to be present on the report but not found."
        Expect.containsAll [|"A";"B";"C"|] (snd result.[10]) "Expected to find all A,B and C"
       
        let A_Gets_Everydays = result |> List.forall (fun t -> (snd t) |> Array.contains "A")
        Expect.isTrue A_Gets_Everydays "Expected A to be found on all days"
       
        //C will get only on Tuesdays and Fridays
        let C_getsOnlyOnTuesdays = result |> List.filter(fun (day, users) -> 
                                              DateTime.Parse(day).DayOfWeek = DayOfWeek.Tuesday || 
                                              DateTime.Parse(day).DayOfWeek = DayOfWeek.Friday)
                                          |> List.forall(fun (_, users ) -> users |> Array.contains "C")
        Expect.isTrue C_getsOnlyOnTuesdays "C is expected to get reports only on Tuesdays and Fridays" 

        
        //Validating the whole report 
        //One row at a time 
        let expected = [("4/1/2018",[|"A"|]);
                        ("4/2/2018",[|"A"|]);
                        ("4/3/2018",[|"A";"C"|]);
                        ("4/4/2018",[|"A"|]);
                        ("4/5/2018",[|"A"|]);
                        ("4/6/2018",[|"A";"C"|]);
                        ("4/7/2018",[|"A"|]);
                        ("4/8/2018",[|"A"|]);
                        ("4/9/2018",[|"A"|]);
                        ("4/10/2018",[|"A";"C";"B"|]);
                        ("4/11/2018",[|"A"|]);
                        ("4/12/2018",[|"X"|]);
                        ("4/13/2018",[|"A";"C"|]);
                        ("4/14/2018",[|"A"|])]

        Expect.equal result expected  "Something wrong in the report"
        //Validating individual parts of the report 
        Expect.equal "4/1/2018" (fst result.[0]) "The report must start on 4/1/2018" 
        Expect.equal "4/14/2018" (fst result.[result.Length - 1]) "The report must end on 4/14/2018" 
        Expect.equal (snd result.[0]) [|"A"|] "Only A is expected to get the marketing material on 4/1/2018" 
        Expect.equal (snd result.[1]) [|"A"|] "Only A is expected to get the marketing material on 4/2/2018" 
        Expect.equal (snd result.[2]) [|"A";"C"|] "A and C are expected to get the marketing material on 4/3/2018" 
        Expect.equal "4/4/2018" (fst result.[3]) "Fourth day of the report is 4/4/2018"
        Expect.equal (snd result.[3]) [|"A"|] "Only A is expected to get the marketing material on 4/4/2018" 


      };
        
      test "Never means Never" {
        let path = "C:\Users\Admin\Documents\GitHub\CustomerPreferenceCentre\choices_never.txt"
        let startDate = new DateTime(2018,4,1)
        let howManyDays = 13
        let result = CustomPrefCentreLib.Facade.solve path startDate howManyDays

        Expect.equal 14 result.Length "Expected 14 rows in the report."
        Expect.isTrue (result |> List.forall (fun t -> (snd t).Length = 0)) "Nothing is expected "
      };
    
      ]
 
  
   


[<EntryPoint>]
let main args =
  runTestsWithCLIArgs [] args tests