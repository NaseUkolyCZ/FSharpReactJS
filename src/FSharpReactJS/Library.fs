namespace FSharpReactJS

type User = 
    {
        FirstName: string
        LastName: string
    }

type Res = 
    {
        choices : string array
    }

/// Documentation for my library
///
/// ## Example
///
///     let h = Library.hello 1
///     printfn "%d" h
///
module Library = 
  
      /// Returns 42
      ///
      /// ## Parameters
      ///  - `num` - whatever
    let hello num = 42
    let mutable values : Res = { choices = [|"value1";"value2"|] }

