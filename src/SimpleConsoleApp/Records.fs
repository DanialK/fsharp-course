namespace LanguageOverview

module Records = 
    type PersonRecord = 
        {
            FirstName : string
            LastName : string
            Age : int
        }

    let danial = 
        {
            FirstName = "Danial"
            LastName = "Khosravi"
            Age = 23
        }
