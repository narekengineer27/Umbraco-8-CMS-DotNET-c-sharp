### Initial Run for Umbraco Web to install Connector Plugin

- Restore the Database file (included in the Database folder for the solution) [either **umbraco.cms.8.0.2-initial.bak** or **umbraco.cms.8.0.2-initial.bacpac**]
- Run the project *(F5)* to complete the installation (Reference to Connector has already been added).
- Once it completes, it will as to reload the project and stop debugging. Say YES to both. [this step will add the global.asax and dependency to the project, and it will require stopping]
- Run the project *(F5)* again

##### Use [Postman](https://www.getpostman.com/downloads/) to test the Rpc endpoints (see exported collection)

###### When deploying to the Production Server, make sure IIS has full permissions for the user IIS_IUSRS in the Umbraco Website Folder

**remarks**
- If you are not using Visual Studio 2017 Enterprise, please update the MSBUILD path in the *ResourceHelper.cs* file, line 106
    
        Environment.SetEnvironmentVariable("VSINSTALLDIR", "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Enterprise\\");

This is a necessary hack due to a bug with MSBuild and Visual Studio 2017, refer to [Internal MSBuild Error: could not load type Microsoft.Build.Utilities.ToolLocationHelper #1000](https://github.com/Microsoft/msbuild/issues/1000)

If the automated global.asax.cs include fails. open the Umbraco.Cms.Web.8.0.2.csproj file in Notepad, or Notepad++ or whatever text editor, and add these lines:

        <ItemGroup>
            <Compile Include="Global.asax.cs">
              <DependentUpon>Global.asax</DependentUpon>
            </Compile>
        ....

Add this right above the line where it has

    <Compile Include="Properties\AssemblyInfo.cs" />


So, the final section should look like this

      <ItemGroup>
        <Compile Include="Global.asax.cs">
          <DependentUpon>Global.asax</DependentUpon>
        </Compile>
        <Compile Include="Properties\AssemblyInfo.cs" />
      </ItemGroup>

Once back into Visual Studio, it will prompt you to reload the project, click YES.

For using a different SQL server, change the web.config, line 49

        <connectionStrings>
            <remove name="umbracoDbDSN"/>
            <add name="umbracoDbDSN" connectionString="Server=.\sqlexpress2014;Database=umbraco.cms.8.0.2;Integrated Security=true" providerName="System.Data.SqlClient"/>
          </connectionStrings>

Change where it reads **.\sqlexpress2014** to whatever server you need.

Then you can fun *(F5)* again

###### The default admin is 
 - username: carlos.casalicchio@gmail.com
 - password: tWA5@hr3Gug6Ahq