# CORE mvc ordering test progect

to test:
-------------------------------------------------------
- add dotnet packages
- install npm pakets
- npx webpack to build JS
- build net core
- run
(change ISS port in proj configs on IIS start profile)

integration tests run:
-------------------------------------------------------
- dotnet test tests\integrationtests\integrationtests.csproj


test url after start:
-------------------------------------------------------
> http://localhost:5002/TestArea/SignalRwork/work
> http://localhost:5002/TestArea/React/CheckShoppingList
> http://localhost:5002/TestArea/Home


Related files:
-------------------------------------------------------
> view
- API\Areas\TestArea\Views\SignalRWork\work.cshtml
> controller
- API\Areas\TestArea\FolderControllers\SignalRWorkController.cs
> JS
- wwwroot\js\Libs\signalR\signalRwork.js
> model for payload on back
- Domain\Models\WorkStatus.cs
> back C# code
- Infrastructure\SignalR\SignalRWorks.cs


## Pckages to build core:
-------------------------------------------------------
```
    dotnet add crmvcsb\mvccoresb.csproj package Newtonsoft.Json --version 12.0.2
    dotnet add crmvcsb\mvccoresb.csproj package Autofac.Extensions.DependencyInjection --version 4.4.0
    dotnet add crmvcsb\mvccoresb.csproj package AutoMapper --version 8.1.0
    dotnet add crmvcsb\mvccoresb.csproj package AutoMapper.Extensions.Microsoft.DependencyInjection --version 6.1.0
    dotnet add crmvcsb\mvccoresb.csproj package Microsoft.AspNetCore.SignalR
	dotnet add crmvcsb\mvccoresb.csproj package FluentValidation.AspNetCore
	
	dotnet add tests\integrationtests\integrationtests.csproj package xunit --version 2.4.1
	dotnet add tests\integrationtests\integrationtests.csproj package xunit.runner.visualstudio --version 2.4.1
	dotnet add tests\integrationtests\integrationtests.csproj package AngleSharp --version 0.12.1
	dotnet add tests\integrationtests\integrationtests.csproj package FluentAssertions --version 5.7.0
	dotnet add tests\integrationtests\integrationtests.csproj package Microsoft.NET.Test.Sdk --version 16.2.0
	dotnet add package Serilog.AspNetCore
	

```

## Migrate from net core 2.0 to 3.0
-------------------------------------------------------
```
dotnet add G:\disk\Files\git\Core\crmvcsb\crmvcsb\crmvcsb.csproj package Microsoft.EntityFrameworkCore.SqlServer --version 3.0.0
dotnet add G:\disk\Files\git\Core\crmvcsb\crmvcsb\crmvcsb.csproj package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore --version 3.0.0
dotnet add G:\disk\Files\git\Core\crmvcsb\crmvcsb\crmvcsb.csproj package Microsoft.EntityFrameworkCore.Abstractions --version 3.0.0
dotnet add G:\disk\Files\git\Core\crmvcsb\crmvcsb\crmvcsb.csproj package Microsoft.EntityFrameworkCore.Analyzers --version 3.0.0
dotnet add G:\disk\Files\git\Core\crmvcsb\crmvcsb\crmvcsb.csproj package Microsoft.EntityFrameworkCore.InMemory --version 3.0.0
dotnet add G:\disk\Files\git\Core\crmvcsb\crmvcsb\crmvcsb.csproj package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 3.0.0
```

## Pckages to build js:
-------------------------------------------------------
```
    npm install --save-dev react react-dom
    npm install --save-dev gulp gulp-babela
    npm install --save-dev webpack webpack-dev-server webpack-cli webpack-stream html-webpack-plugin clean-webpack-plugin
    npm install --save-dev @babel/core @babel/cli @babel/plugin-proposal-class-properties @babel/preset-env @babel/preset-react @babel/plugin-transform-arrow-functions @babel/plugin-transform-classes @babel/plugin-proposal-function-bind
```

## Decomposition
-------------------------------------------------------



### API (for fiddler test method,url,attribute,body)
-------------------------------------------------------




startup.cs changes
-------------------------------------------------------
custom default MVC Area location folder in API/Areas
in startup.cs rerouted through  RazorViewEngineOptions

Autofac container registration added

AutoMapper service added, 
one coniguration 
two types of initialization - static and instance API

AutoFact to Automapper registration added

AutofacServiceProvider returned from ConfigureServices

Autofac multiple Irepositories registration
https://autofaccn.readthedocs.io/en/latest/faq/select-by-context.html

-------------------------------------------------------
Added http instead of https routing for Fiddler test to:
    .UseUrls("http://localhost:5002")
