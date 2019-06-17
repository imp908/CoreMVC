# CORE mvc chat test progect

Тестовый чат на SignalR с базовой аутентификацией на razor pages.
Миграции для sql в папке Migrations.
Стандартная аутентификация позволяет регитрироваться, логиниться и управоять учёткой,
зарегистрированный пользователь может зайти в чат
/Identity/Chat/RoomP
Сообщения не сохраняются.


## Pckages to build core:
-------------------------------------------------------
```
    dotnet add package Newtonsoft.Json --version 12.0.2
    dotnet add package Autofac.Extensions.DependencyInjection --version 4.4.0
    dotnet add package AutoMapper --version 8.1.0
    dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 6.1.0
    dotnet add package Microsoft.AspNetCore.SignalR
```

## Pckages to build js:
-------------------------------------------------------
```
    npm install --save-dev react react-dom
    npm install --save-dev gulp gulp-babela
    npm install --save-dev webpack webpack-dev-server webpack-cli webpack-stream html-webpack-plugin clean-webpack-plugin
    npm install --save-dev @babel/core @babel/cli @babel/plugin-proposal-class-properties @babel/preset-env @babel/preset-react @babel/plugin-transform-arrow-functions @babel/plugin-transform-classes @babel/plugin-proposal-function-bind
```

## Files
-------------------------------------------------------

- Authentication contoller
- Controllers\HomeController.cs

- chat controller signalR chat:
- API\Areas\TestArea\FolderControllers\SignalRcontroller.cs

- chat JS signal r client
- G:\disk\Files\git\Core\crmvcsb\wwwroot\js\Libs\signalR\signalr.js

### API (for test)

-------------------------------------------------------
- Homepage
- http://localhost:5002/Home/Index
- Register user
- http://localhost:5002/Identity/Account/Register
- login 
- http://localhost:5002/Identity/Account/Login
- Chat private room
- http://localhost:5002/Identity/Chat/RoomP


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


Program.cs changes
-------------------------------------------------------
Added http instead of https routing for Fiddler test to:
    .UseUrls("http://localhost:5002")


