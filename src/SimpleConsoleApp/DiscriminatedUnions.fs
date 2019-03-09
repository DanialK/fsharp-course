namespace LanguageOverview

open System

module DescriminatedUions = 
    type TrafficSignals = 
        | Red
        | Yellow
        | Green

    type Shape = 
        | Circle of float
        | Rectangle of float * float
        | Square of float

    type MessageRecieverState = 
        | Off
        | Activating of WhenActivated : DateTime
        | Idle of IdleDuration : TimeSpan
        | MessageRecieved of Message : string * WhenRecieved : DateTime
        | Deactivating of WhenDeactivated : DateTime

