module DataProcessing

open LoanPayments

[<EntryPoint>]
let main argv =
    let result = paymentData
    printfn "%A" (Array.head result)
    0 // return an integer exit code
