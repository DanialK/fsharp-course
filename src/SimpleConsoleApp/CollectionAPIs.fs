namespace LanguageOverview

module CollectionAPIs = 
    let testList = List.init 20 (fun n -> n * 5)

    List.length testList

    testList |> List.isEmpty
    testList |> List.min
    
    testList |> List.exists (fun n -> n = 55)
    testList |> List.forall (fun n -> n = 55)
    
    testList |> List.find (fun n -> n = 55) // Throws if cant find
    testList |> List.tryFind (fun n -> n = 55)
    testList |> List.tryFind (fun n -> n = 550)

    testList |> List.filter (fun n -> n % 3 = 0)

    // Compose
    testList
    |> List.filter (fun n -> n % 3 = 0)
    |> List.map (fun n -> n * n)
    |> List.rev

    testList
    |> List.iter (fun n -> printfn "%i" n)

    testList
    |> List.iter (printfn "%i")


    testList
    |> List.fold (fun acc n -> if n % 3 = 0 then acc else n :: acc) []

    testList
    |> List.reduce (fun acc n -> if n % 3 = 0 then acc else acc + 3)

    testList
    |> List.toArray

    testList
    |> Array.ofList
