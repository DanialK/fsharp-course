module SequenceOps
    1
    |> Seq.unfold(fun n ->
        if n < 300
            then Some(n, n + n)
            else None)
    |> Seq.iter (printf "%d ")

    // Fibonacci using unfold
    (0, 1)
    |> Seq.unfold( fun (n1, n2) -> Some(n1 + n2, (n2, n1 + n2)) )
    |> Seq.take 15
    |> Seq.iter (printf "%d ")
    
    // Fibonacci using seq expressions
    seq {
        let rec fibonacci (n1, n2) =
            seq {
                let x = n1 + n2
                yield x
                yield! fibonacci(n2, x)
            }
        yield! fibonacci(0, 1)
    }
    |> Seq.take 15
    |> Seq.iter (printf "%d ")

    // Concat two collections
    let list1 = [ 1 .. 5 ]
    let list2 = [ 6 .. 10 ]
    let combined = List.append list1 list2

    let squareAndCube n = 
        [
            n * n
            n * n * n
        ]
    
    // collect is flatMap
    combined
    |> List.collect squareAndCube
    |> List.iter (printf "%d ")


    let zipped = List.zip list1 list2
    let unzipped = List.unzip zipped
    List.append (fst unzipped) (snd unzipped)

    // Lists that get zipped must have the same size
    List.zip list1 combined // this will throw
    // Seqs of different size can be zipped and the size would be equal to the size of the samllest seq
    Seq.zip list1 combined

    // Grouping elements based on their index position
    list1 |> List.pairwise // list of tuples
    list1 |> List.windowed 2 // list of lists
    list1 |> List.windowed 3 // lits of lists
    
    let petBreeds = 
        [
            ("Cat", "Persian")
            ("Dog", "Collie")
            ("Cat", "Russian Blue")
            ("Bird", "Canary")
            ("Dog", "Corgie")
            ("Cat", "Siamese")
        ]
    petBreeds
    |> List.groupBy (fun t -> fst t)

