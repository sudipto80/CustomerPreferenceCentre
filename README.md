# Customer Preference Centre
Customer Preference Centre example

## Problem statement 

Implement a Customer Preference Centre
Customers are able to set their preferences for receiving marketing information. The
following options are available:
  * On a specified date of the month [1-28]
  * On each specified day of the week [MON-SUN] (collection)
  * Every day
  * Never

Implement a system that accepts the choices of multiple customers as input. After receiving
the input the system should produce a report of the upcoming 90 days. For each day that
marketing material will be sent, the report should show which customers will be a recipient.
For example, Customer A chooses 'Every day'. Customer B chooses 'On the 10th of the
month'. Customer C chooses ‘On Tuesday and Friday’. After providing this input the
abridged output beginning in April would be:

![image](https://user-images.githubusercontent.com/1287634/227128061-b6f351ec-80f9-417f-9e36-bf08496406a9.png)

## Input 
The program accepts user choices in form of an input text file like this

![image](https://user-images.githubusercontent.com/1287634/227129286-f1e7cb8d-0e1e-46b5-bd6c-075bc4c9b0fb.png)

Each row represents choice of a particular user. For example the first row represents the choice of user A; which is Everyday. 
Last row represents the choice of the user C. 
