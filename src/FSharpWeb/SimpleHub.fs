namespace Hubs

open Newtonsoft.Json
open FSharpReactJS
open EkonBenefits.FSharp.Dynamic

type SimpleHub() =
    inherit Microsoft.AspNet.SignalR.Hub()

    member this.GetData() = 
        let data = JsonConvert.SerializeObject( { FirstName = "John"; LastName = "Doe" } ) 
        this.Clients.All?showLiveResult(data)
