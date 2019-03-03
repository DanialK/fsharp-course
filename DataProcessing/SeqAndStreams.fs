module SeqAndStreams
    open System.IO
    
    seq { 1 .. 3 }
    |> Seq.iter (printfn "%d")

    seq { for i in 1 .. 3 -> i*i }
    |> Seq.iter (printfn "%d")

    seq { for i in 1 .. 3 do yield i*i }
    |> Seq.iter (printfn "%d")

    seq {
        for i in 1 .. 3 do
            for j in 4 .. 5 do
                yield i + j
    }
    |> Seq.iter (printfn "%d")


    // yield vs yield! (append vs concat)
    seq {
        for i in 1 .. 3 do
            yield i
        yield! seq { 4 .. 5}
    }
    |> Seq.iter (printfn "%d")

    let rec listAllFiles (path: string) =
        seq {
            for file in Directory.GetFiles(path) do
                yield file
            for directory in Directory.GetDirectories(path) do
                yield! listAllFiles directory
        }

    listAllFiles @"C:\dev"
    |> Seq.iter (printfn "%s")

    // Streams
    let readSingleLines (filePath: string) =
        seq {
            let rec readLine (reader: StreamReader) = 
                seq {
                    if reader.EndOfStream = false
                        then
                            yield reader.ReadLine()
                            yield! readLine reader
                }
            use stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None)
            let reader = new StreamReader(stream)
            yield! readLine reader
        }
    let lines = readSingleLines @"C:\dev\fsharp-course\DataProcessing\Loan payments data.csv"
    let enumerator = lines.GetEnumerator()
    enumerator.MoveNext()
    enumerator.Current
    enumerator.Dispose()



