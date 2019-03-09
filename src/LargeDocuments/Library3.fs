
module Memoization
    open System.IO
    open System.Collections.Generic

    let wikiFile = @"C:\dev\fsharp-course\simplewiki-20170820-pages-meta-current.xml"

    let readLines filename =
        filename
        |> File.ReadLines
    
    let readWordsInLine (line: string) = 
        line.Split([| ' '; '_'; '\\'; '/'; ' ' |])
        |> Seq.ofArray
        |> Seq.map (fun s -> s.Trim())

    let readWrodsInFile filename = 
        filename
        |> readLines
        |> Seq.collect readWordsInLine

    let wikiWords = readWrodsInFile wikiFile

    wikiWords |> Seq.contains "Australia"

    wikiWords |> Seq.contains "LexiSession"


    let isWordInSequence (words: string seq) =
        let wordSet = new HashSet<string>()
        let enumerator = words.GetEnumerator()
        fun word ->
            if wordSet.Contains word
                then true
                else    
                    let mutable isFound = false
                    while enumerator.MoveNext() && (not isFound) do
                        let w = enumerator.Current
                        wordSet.Add w |> ignore
                        if w = word
                            then isFound <- true
                    isFound

    let isWordInWiki = isWordInSequence wikiWords
    isWordInWiki "Australia"
    isWordInWiki "LexiSession" // 46s
    isWordInWiki "LexiSession" // 0s

    let countWordInSequence words =
        let wordCounts = new Dictionary<string, int>()
        words
        |> Seq.iter(fun word ->
            if wordCounts.ContainsKey(word)
                then wordCounts.[word] <- wordCounts.[word] + 1
                else wordCounts.[word] <- 1)
        fun word ->
            if wordCounts.ContainsKey(word)
                then wordCounts.[word]
                else 0
    let countWordsInWiki = countWordInSequence wikiWords    
    countWordsInWiki "Wiki"
