// Learn more about F# at http://fsharp.org

open Parsing
open FParsec

let testParser parser = 
    let innerFunc input = 
        match run parser input with
            | Success (result, _, _) ->
                printfn "Success: %A" result
            | Failure (error, _, _) ->
                printfn "Error: %s" error
    innerFunc

[<EntryPoint>]
let main argv =
    testParser parseItemData "\"P123\", \"Bongos\", \"1\", \"68.01\""
    testParser parseOrderData "\"1234\", \"Jane Miller\", \"2017-03-23\", \"FedEx\""
    testParser parseLine "\"I\", \"P123\", \"Bongos\", \"1\", \"68.01\""
    testParser parseLine "\"O\", \"1234\", \"Jane Miller\", \"2017-03-23\", \"FedEx\""
    testParser parseLines input
    0 // return an integer exit code
