module FSharpReactJS.Tests

open FSharpReactJS
open NUnit.Framework
open Microsoft.SharePoint.Client

let guidStr() =
    System.Guid.NewGuid.ToString()

[<Test>]
let ``added Employee gets returned`` () =
    let lib = new EmployeeLibrary()
    let employee = lib.AddEmployee(guidStr(), guidStr())
    Assert.Contains(
        employee, 
        (new System.Collections.Generic.List<Employee>(lib.Employees))
    )