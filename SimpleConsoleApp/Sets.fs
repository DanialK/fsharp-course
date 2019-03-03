namespace LanguageOverview

module Sets = 
    let set1 = set [ 1 ; 2 ; 2;  3 ]

    let set2 = set1.Add 8
    
    set1.IsProperSubsetOf set2
    set1.IsProperSubsetOf set1
