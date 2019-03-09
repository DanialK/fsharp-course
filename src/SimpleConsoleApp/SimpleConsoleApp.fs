module SimpleConsoleApp

open LanguageOverview.Records

[<EntryPoint>]
let main argv =
    printfn "Hello %s" danial.FirstName
    0 // return an integer exit code
