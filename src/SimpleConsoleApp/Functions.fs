namespace LanguageOverview

module Functions = 
    let func1 x =
        x * x

    let func2 (x : float) =
        x * x

    let func3 x : float =
        x * x

    let func4 (x : float) : float =
        x * x
    
    let func5 () =
        printfn "Hello functions!"

    let func6 x y =
        x * y
    
    let func7 = func6 11

    let func8 (x, y) = 
        x * y

    let func9 () =
        (fun x -> x * x)
    
    func9() 10

    let func10 f x =
        f x

    func10 (fun x -> x + 1) 10
    10 |> func10 (fun x -> x + 1)

