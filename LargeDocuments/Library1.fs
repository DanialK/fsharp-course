
module Timeing
    (*
    #time
    *)
    let f = 
        fun () ->
            for i in [1 .. 100] do printf "%d" i
    
    f()


    let timeAFunction func =
        let stopWatch = System.Diagnostics.Stopwatch.StartNew()
        let result = func()
        stopWatch.Stop()
        (stopWatch.ElapsedMilliseconds, result)
    
    timeAFunction f