namespace Hubs

open Newtonsoft.Json
open FSharpReactJS
open EkonBenefits.FSharp.Dynamic

type EmployeeHub() as this =
    inherit Microsoft.AspNet.SignalR.Hub()

    let library = EmployeeLibrary()

    let serializeEmployees() =
        JsonConvert.SerializeObject(library.Employees)

    let refreshAllClients() =
        this.Clients.All?showEmployees(serializeEmployees()) |> ignore

    let rec refreshTask() = async {
        do! Async.Sleep(10000)
        refreshAllClients()
        return! refreshTask()
    }

    do
        Async.StartAsTask(refreshTask()) |> ignore

    member this.AddEmployee(firstName : string, lastName : string) = 
        library.AddEmployee(firstName, lastName) |> ignore
        refreshAllClients()

    member this.GetEmployees() =
        this.Clients.Caller?showEmployees(serializeEmployees()) |> ignore
