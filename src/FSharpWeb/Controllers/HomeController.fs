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
        let accessToken = this.Request.[ "accessToken" ]
        this.Session.["accessToken"] <- accessToken

        let endpointRequest : HttpWebRequest  = downcast HttpWebRequest.Create("https://naseukolyczsro.sharepoint.com/_api/web/lists")
        endpointRequest.Method <- "GET";
        endpointRequest.Accept <-  "application/json;odata=verbose"
        endpointRequest.Headers.Add("Authorization", 
          "Bearer " + accessToken );
        let endpointResponse : HttpWebResponse = downcast endpointRequest.GetResponse()
        let stream = endpointResponse.GetResponseStream()
        use reader = new StreamReader(stream) 
        let content = reader.ReadToEnd()
        Debug.WriteLine("content")
        Debug.WriteLine(content)
        Debug.WriteLine("-------------------------------------------------------------------------------")
        this.View()

