namespace FSharpWeb.Controllers
open System
open System.Collections.Generic
open System.Linq
open System.Net.Http
open System.Web.Http
open FSharpReactJS

/// Retrieves values.
[<RoutePrefix("api2/values")>]
type ValuesController() =
    inherit ApiController()

    /// Gets all values.
    [<Route("")>]
    member x.Get() = (new EmployeeLibrary()).Employees

