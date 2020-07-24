### Install Umbraco Web

- Create a new Visual Studio Project, using [.NET Framework 4.7.2](http://go.microsoft.com/fwlink/?linkid=863265)
- Install Umbraco Cms 

        Install-Package UmbracoCms -Version 8.0.2

- Run the project to complete the installation (Database).
- Choose Sql Server (multi language does not work with SQLCE)

	- **Do not install any starter kits nor Machine Key**

- install the [json-rpc](https://github.com/Astn/JSON-RPC.NET/blob/master/README.md) nuggets in the umbraco project


        Install-Package AustinHarris.JsonRpc

        Install-Package AustinHarris.JsonRpc.AspNet



##### Once completed the initial installation, in the Umbraco Web Project:

- Add to Web.config the AppId and Api Key

        <add key="TotalCode.Admin.AppId" value="4d53bce03ec34c0a911182d4c228ee6c"/>
        <add key="TotalCode.Admin.ApiKey" value="A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabc="/>

- Add to Web.config the JsonRpc handlers

      <handlers>
          <add name="jsonrpc" type="AustinHarris.JsonRpc.Handlers.AspNet.JsonRpcHandler" verb="*" path="*.rpc"/>
       </handlers>

- Add a new Global.asax file (delete the original one)

        <%@ Application Codebehind="Global.asax.cs" Inherits="Umbraco.Cms.Web._802_MSSQL.Global" Language="C#" %>

  - In the Code Behind

        namespace Umbraco.Cms.Web._802_MSSQL
        {
            using Umbraco.Plugins.Connector.Controllers;
            using Umbraco.Web;
            public class Global : UmbracoApplication
            {
                private JsonRpcController service;

                public override void Init()
                {
                    try
                    {
                        service = service ?? new JsonRpcController();
                    }
                    catch
                    {
                    }
                }
            }
        }

- Run the Web Project (F5)

##### Use [Postman](https://www.getpostman.com/downloads/) to test the Rpc endpoints (see exported collection)

###### When deploying to the Production Server, make sure IIS has full permissions for the user IIS_IUSRS in the Umbraco Website Folder