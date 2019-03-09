module LoanPayments

open System
open System.IO

let lines =
    File.ReadAllLines(@"Loan payments data.csv")
    |> Array.distinct
    |> Array.map (fun s -> s.Split(','))

let header = lines |> Array.head

let data = lines |> Array.tail


[<Measure>] type dollar
[<Measure>] type terms
[<Measure>] type age
[<Measure>] type days

type LoanStatus =
    | PaidOff of PaidOffTime : DateTime option
    | Collection of PastDueDays : int<days>
    | CollecttonPaidOff of PaidOffTime : DateTime option * PastDueDays : int<days>

type Education = 
    | HighSchoolOrBelow
    | College 
    | MasterOrAbove

type Gender = 
    | Male
    | Female
    
type LoanPaymentData =
    {
        LoanId : string;
        LoanStatus : LoanStatus;
        Principal : int<dollar>;
        Terms : int<terms>;
        EffectiveDate : DateTime option;
        DueDate : DateTime option;
        Age : int<age>;
        Education : Education;
        Gender : Gender
    }

let tryParseDate (dateString: string) =
    let couldParse, parsedDate = DateTime.TryParse(dateString)
    if couldParse then Some(parsedDate) else None

let transformToLoanStatus (status, paidOffTime, pastDueDays) = 
    match status with
    | "PAIDOFF" ->
        PaidOff(tryParseDate paidOffTime)
    | "COLLECTION" ->
        Collection((Int32.Parse(pastDueDays)) * 1<days>)
    | "COLLECTION_PAIDOFF" ->
        CollecttonPaidOff(tryParseDate paidOffTime, (Int32.Parse(pastDueDays)) * 1<days>)
    | unknown ->
        failwith (sprintf "Unrecognized loan status: \"%s\"" unknown)


let transformToEducation = function
    | "High School or Below" ->
        HighSchoolOrBelow
    | "Bachelor"
    | "Bechalor"
    | "college" ->
        College
    | "Master or Above" ->
        MasterOrAbove
    | unknown ->
        failwith (sprintf "Unrecognized education: \"%s\"" unknown)

let transformToGender = function
    | "male" ->
        Male
    | "female" ->
        Female
    | unknown ->
        failwith (sprintf "Unrecognized gender: \"%s\"" unknown)

let transformToLoanPaymentData (row : string []) =
    {
        LoanId = row.[0];
        LoanStatus = transformToLoanStatus (row.[1], row.[6], row.[7]);
        Principal = Int32.Parse(row.[2]) * 1<dollar>;
        Terms = Int32.Parse(row.[3]) * 1<terms>;
        EffectiveDate = tryParseDate row.[4];
        DueDate =  tryParseDate row.[5];
        Age = Int32.Parse(row.[8]) * 1<age>;
        Education = transformToEducation row.[9];
        Gender = transformToGender row.[10]
    }

let paymentData = 
    data
    |> Array.map transformToLoanPaymentData
