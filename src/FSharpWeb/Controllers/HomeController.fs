namespace FSharpWeb.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Web
open System.Web.Mvc
open System.Web.Mvc.Ajax
open System.Net
open System.IO
open System.Diagnostics

type HomeController() =
    inherit Controller()
    member this.Index () = 
        this.View()

