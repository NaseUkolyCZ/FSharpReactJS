namespace FSharpReactJS
open Microsoft.SharePoint.Client

type Employee = {
    Id: int
    FirstName: string
    LastName: string
}

type EmployeeLibrary() =

    let unsafeToSecureString str =
        let unsafe = new System.Security.SecureString()
        for c in str do unsafe.AppendChar(c)
        unsafe

    let credentials = 
        new SharePointOnlineCredentials(
            "user@NaseUkolyCZsro.onmicrosoft.com", 
            (unsafeToSecureString "uA1DtQF0e18V")
        )

    let clientContext = 
        let ctx = new ClientContext("https://naseukolyczsro.sharepoint.com/")
        ctx.Credentials <- credentials
        ctx

    let library = clientContext.Web.Lists.GetByTitle("Employees")

    let employeeFromItem (item : ListItem) = {
        Id = item.Id
        FirstName = downcast item.["FirstName"]
        LastName = downcast item.["LastName"]
    }

    let allItemsQuery = new CamlQuery()

    member this.AddEmployee(firstName, lastName) =
        let item = library.AddItem(new ListItemCreationInformation())
        item.["Title"] <- (sprintf "%s %s" firstName lastName)
        item.["FirstName"] <- firstName
        item.["LastName"] <- lastName
        item.Update()
        clientContext.ExecuteQuery()
        employeeFromItem item

    member this.Employees 
        with get() =
            let items = 
                clientContext.LoadQuery(
                    library.GetItems(allItemsQuery).Include(
                        (fun i -> upcast i.Id),
                        (fun i -> i.["FirstName"]),
                        (fun i -> i.["LastName"])
                    )
                )
            clientContext.ExecuteQuery()
            Seq.map employeeFromItem items
