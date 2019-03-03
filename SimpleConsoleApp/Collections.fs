namespace LanguageOverview

module Collections = 
    let array1 = [| 1 .. 10 |]
    let list1 = [ 1 .. 10 ]
    let seq1 = seq { 1 .. 2}
    let map1 = Map.empty
                .Add(1, "One")
                .Add(2, "Two")
                .Add(3, "Three")
