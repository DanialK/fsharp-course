module Parsing

open System
open System.IO
open FParsec

type Order = 
    {
        OrderNumber : string
        CustomerName : string
        OrderDate : DateTime
        ShipVia : string
        Items : Item list
    }
and Item = 
    {
        ProductNumber : string
        ProductName : string
        Quantity : int32
        Price : decimal
    }

type ParsedData =
    | OrderData of Order
    | ItemData of Item

let input = File.ReadAllText("Orders.csv")

let dataInQuotes p = 
    between (skipChar<unit> '"') (skipChar<unit> '"') p

let intInQuotes = 
    dataInQuotes pint32

let stringInQuotes =
    dataInQuotes (manyChars (satisfy (fun c -> c <> '"')))

let decimalInQuotes = stringInQuotes |>> decimal

let dateTimeInQuotes = 
    stringInQuotes
    |>> fun s -> DateTime.ParseExact(s, "yyyy-mm-dd", null)

let skipCommaDelimiter p =
    p .>> skipChar<unit> ','.>> spaces


let parseItemData = 
    pipe4
        (stringInQuotes |> skipCommaDelimiter)
        (stringInQuotes |> skipCommaDelimiter)
        (intInQuotes |> skipCommaDelimiter)
        decimalInQuotes
        (fun productNumber productName quantity price -> 
            ItemData(
                {
                    ProductNumber = productNumber
                    ProductName = productName
                    Quantity = quantity
                    Price = price
                })
            )

let parseOrderData = 
    pipe4
        (stringInQuotes |> skipCommaDelimiter)
        (stringInQuotes |> skipCommaDelimiter)
        (dateTimeInQuotes |> skipCommaDelimiter)
        stringInQuotes
        (fun orderNumber customerName orderDate shipVia -> 
            OrderData(
                {
                    OrderNumber = orderNumber
                    CustomerName = customerName
                    OrderDate = orderDate
                    ShipVia = shipVia
                    Items = []
                })
            )

let parseLine = 
    dataInQuotes (pchar<unit> 'O' <|> pchar<unit> 'I')
    |> skipCommaDelimiter
    >>= fun c ->
            if c = 'O' then parseOrderData else parseItemData

let parseLines = 
    sepBy parseLine newline .>> eof