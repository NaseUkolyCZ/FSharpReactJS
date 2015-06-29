namespace Hubs

open Newtonsoft.Json
open FSharpReactJS
open EkonBenefits.FSharp.Dynamic

type EmployeeHub() =
    inherit Microsoft.AspNet.SignalR.Hub()

    let library = EmployeeLibrary()

    let serializeEmployees() =
        JsonConvert.SerializeObject(library.Employees)

    member this.AddEmployee(firstName : string, lastName : string) = 
        library.AddEmployee(firstName, lastName) |> ignore
        this.Clients.All?showEmployees(serializeEmployees()) |> ignore

    member this.GetEmployees() =
            this.Clients.Caller?showEmployees(serializeEmployees()) |> ignore