namespace Hubs

open Newtonsoft.Json
open FSharpReactJS
open EkonBenefits.FSharp.Dynamic

type SimpleHub() =
    inherit Microsoft.AspNet.SignalR.Hub()

    member this.Send( choice : string ) = 
        Library.values <- { choices = Seq.append Library.values.choices ( seq [| choice + "1" |] ) |> Seq.toArray }
        this.Clients.All?showLiveResult(JsonConvert.SerializeObject(Library.values)) |> ignore
