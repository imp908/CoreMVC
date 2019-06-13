## CORE mvc template test progect

to test:
-------------------------------------------------------
- add dotnet packages
- install npm pakets
- npx webpack to build JS
- build net core
- run

test url after start:
-------------------------------------------------------
> http://localhost:5002/TestArea/SignalRwork/work


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


Pckages to build core:
-------------------------------------------------------
    dotnet add package Newtonsoft.Json --version 12.0.2
    dotnet add package Autofac.Extensions.DependencyInjection --version 4.4.0
    dotnet add package AutoMapper --version 8.1.0
    dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 6.1.0
    dotnet add package Microsoft.AspNetCore.SignalR

Pckages to build js:
-------------------------------------------------------
    npm install --save-dev react react-dom
    npm install --save-dev gulp gulp-babela
    npm install --save-dev webpack webpack-dev-server webpack-cli webpack-stream html-webpack-plugin clean-webpack-plugin
    npm install --save-dev @babel/core @babel/cli @babel/plugin-proposal-class-properties @babel/preset-env @babel/preset-react @babel/plugin-transform-arrow-functions @babel/plugin-transform-classes @babel/plugin-proposal-function-bind



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


-------------------------------------------------------
Added http instead of https routing for Fiddler test to:
    .UseUrls("http://localhost:5002")



