module ReadWikipedia
    open System.IO
    (*
    #time
    *)
    let wikiFile = @"C:\dev\fsharp-course\simplewiki-20170820-pages-meta-current.xml"

    let readAllLines filename =
        filename
        |> File.ReadAllLines
        |> Seq.ofArray

    let readLines filename =
        filename
        |> File.ReadLines

    let readSingleLines (filePath: string) =
        seq {
            let rec readLine (reader: StreamReader) = 
                seq {
                    let line = reader.ReadLine()
                    if not (isNull line)
                        then
                            yield reader.ReadLine()
                            yield! readLine reader
                }
            use stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None)
            let reader = new StreamReader(stream)
            yield! readLine reader
        }


    wikiFile |> readAllLines |> Seq.length // 9s
    wikiFile |> readLines |> Seq.length // 4s
    wikiFile |> readSingleLines |> Seq.length // 6s
