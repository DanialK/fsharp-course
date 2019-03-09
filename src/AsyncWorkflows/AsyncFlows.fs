module AsyncFlows

open System
open System.Diagnostics
open System.Threading
open System.Net
open System.Threading.Tasks


let fetchSync url = 
    let uri = Uri(url)
    let client = new WebClient()
    let html = client.DownloadString(uri)
    sprintf "Read %d characters" html.Length

let fetchAsync url = 
    async {
        let uri = Uri(url)
        use client = new WebClient()
        let! html = client.DownloadStringTaskAsync(uri) |> Async.AwaitTask
        return sprintf "Read %d characters" html.Length
    }

let urlList = [
    "http://twitter.com"
    "http://google.com"
    "http://facebook.com"
]


let runAllAsync urls = 
    urls
    |> List.map fetchAsync
    |> Async.Parallel
    |> Async.RunSynchronously

urlList |> runAllAsync