namespace LanguageOverview

module Maps = 
    let map1 =
        Map
            .empty
            .Add(1, "One")
            .Add(2, "Two")
            .Add(3, "Three")

    map1.[2]

    let map2 =
        Map
            .empty
            .Add("One", 1)
            .Add("Two", 2)
            .Add("Three", 3)

    map2.["Two"]