### Automated deploy Umbraco Cms Web to testing server

- Restore the Database file (included in the Database folder for the solution) [either **umbraco.cms.8.0.2-publish.bak** or **umbraco.cms.8.0.2-publish.bacpac**]. **Delete the old DB and restore fresh.**

###### When deploying to the Server, make sure IIS has full permissions for the user IIS_IUSRS to the Umbraco Website Folder

**remarks**

For using a different SQL server, change the web.config, line 49

        <connectionStrings>
            <remove name="umbracoDbDSN"/>
            <add name="umbracoDbDSN" connectionString="Server=.\sqlexpress2014;Database=umbraco.cms.8.0.2;Integrated Security=true" providerName="System.Data.SqlClient"/>
        </connectionStrings>

Change where it reads **.\sqlexpress2014** to whatever server you need.

Then you can fun *(F5)* again

###### The total code admin is 
 - username: totalcode@totalumbraco.com
 - password: >41*_Vkieb

##### Use [Postman](https://www.getpostman.com/downloads/) to test the Rpc endpoints (see exported collection)

**Minimum requirements:**

- Microsoft Sql Server 2014
- Visual Studio 2017
- .NET Framework 4.7.2
- IIS 7+