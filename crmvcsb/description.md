//////////////
///MVC WebApi Folders, routing and URLs:
Folders:
//scaffolded vews for MVC and WebApi
Areas/Scaffolded
//conventional structure but not name
Areas/TestArea/FolderControllers/Homecontroller.cs
//custom controller placement
Areas/TestArea/NewHomecontroller.cs

//controller to check JS bundles
Areas/TestArea/FolderControllers/JScheckController.cs
//view
Areas/TestArea/Views/JScgeck/CheckAppOne.cshtml

//conventional views
Areas/TestArea/Views/Home/Index.cshtml Areas/TestArea/Views/NewHome/Index.cshtml

//React view check
Areas/TestArea/Views/ReactCheck/ReactCheck.cshtml Routes: Added http routing for Fiddler test to: program.cs -> IWebHostBuilder CreateWebHostBuilder -> UseUrls("http://localhost:5000") scaffolded controllers: HomeControllers -> https: //localhost:5001/Scaffolded/home/index
ValuesController -> https: //localhost:5001/api/values
added controllers: HomeController ->
//conventional view
https://localhost:5001/TestArea/Home/
//another folder view
https://localhost:5001/TestArea/Home/NewHomeIndex
NewHomeController ->
//conventional view
https://localhost:5001/TestArea/NewHome/
//another folder view
https://localhost:5001/TestArea/NewHome/OldHomeIndex
BlogController ->
//hardcoded string blog collection
https://localhost:5001/api/blog
//blog object
http://localhost:5000/api/blog/{id}
//get Newtonsoft Jsonized string
http://localhost:5000/api/blog/GetString/{id}

NewOrderController ->
//NEW ORDER

GET
http://localhost:5002/api/NewOrder
http://localhost:5002/api/NewOrder/ReInitialize
http://localhost:5002/api/NewOrder/GetDbName

POST
http://localhost:5002/api/NewOrder/GetCrossRates


Content-Type: application/json

{
  "FromCurrency": "USD",
  "ToCurrency": "RUB",
  "ThroughCurrency": "RUB",
  "Date": "01.01.2019"
}



ValuesController->
//VALUES
//Example of multiple Get with different params per controller
GET
http://localhost:5002/api/Values/GetDbName
http://localhost:5002/api/Values/GetDefault/USD
http://localhost:5002/api/Values/GetCurrency?USD
http://localhost:5002/api/Values/GetCurrencyParam?USD




//POST
Posts add,delete, posts and blog by person, get post by blog 
http://localhost:5000/api/blog/AddPostJSON
{
  "PersonId": "81A130D2-502F-4CF1-A376-63EDEB000E9F",
  "BlogId": "1",
  "Title": "PostTitle",
  "Content": "PostContent"
} 
http://localhost:5002/api/blog/GetPostsByPerson
{ "PersonId": "81A130D2-502F-4CF1-A376-63EDEB000E9F" } 
http://localhost:5002/api/blog/GetPostsByBlog
{ "BlogId": "1" } 
http://localhost:5002/api/blog/GetBlogsByPerson
{ "PersonId": "81A130D2-502F-4CF1-A376-63EDEB000E9F" }

//PUT
http://localhost:5002/api/blog/UpdatePost
{
  "PersonId": "81A130D2-502F-4CF1-A376-63EDEB000E9F",
  "Post": {
    "PostId": "1",
    "Title": "UpdatedTitle",
    "Content": "UpdatedContent"
  }
}

//POST
http://localhost:5002/api/blog/DeletePost
{
  "PersonId": "81A130D2-502F-4CF1-A376-63EDEB000E9F",
  "PostId": "1"
} JScheckController ->
//check Events in AppOne
//uses function Bus,listener and amiter
http://localhost:5002/TestArea/JScheck/CheckAppOne

//check Events in AppTwo
//uses class realization Bus,listener and amiter
http://localhost:5002/TestArea/JScheck/CheckAppTwo

ReactController
//react check
http://localhost:5002/TestArea/React/CheckShoppingList

SignalRcontroller (copypast to several browser windows to test) 
http://localhost:5002/TestArea/SignalR/hub


API: [
  http://localhost:5002/api/blog/AddPost -> returns Ok(result)
  http://localhost:5002/api/blog/AddPostJSON -> retorns Json(result)

  Body:
  {
    "PersonId": "81A130D2-502F-4CF1-A376-63EDEB000E9F",
    "BlogId": "1",
    "Title": "PostTitle",
    "Content": "PostContent"
  }

  PersonAddsPost
  http: /localhost:5000/api/blog/AddPostJSON
  {
    "PersonId": "81A130D2-502F-4CF1-A376-63EDEB000E9F",
    "BlogId": "1",
    "Title": "PostTitle",
    "Content": "PostContent"
  }

  get posts by person personId ->
  List<Posts>
  http://localhost:5002/api/blog/GetPostsByPerson
  { "PersonId": "81A130D2-502F-4CF1-A376-63EDEB000E9F" }

  get posts by blog personId -> List<blogs>
  http://localhost:5002/api/blog/GetPostsByBlog
  { "BlogId": "1" }

  get blogs by person blogId -> List<Posts>
  http://localhost:5002/api/blog/GetBlogsByPerson
  { "PersonId": "81A130D2-502F-4CF1-A376-63EDEB000E9F" }

  person removes post 
  http://localhost:5002/api/blog/UpdatePost
  {
    "PersonId": "81A130D2-502F-4CF1-A376-63EDEB000E9F",
    "Post": {
      "PostId": "1",
      "Title": "UpdatedTitle",
      "Content": "UpdatedContent"
    }
  }

  person updates post
  http://localhost:5002/api/blog/DeletePost
  {
    "PersonId": "81A130D2-502F-4CF1-A376-63EDEB000E9F",
    "PostId": "1"
  }

]

//////////////
attribute vs named area routing OR -> controller routing attribute [ Area("TestArea") ] public class NewHomeController : Controller with template route routes.MapRoute( name:"areas", template:"{area}/{controller}/{action}" ); OR -> NAMED routing in startup.cs wihtout attributes routes.MapAreaRoute( name: "TestArea", areaName: "TestArea", template: "TestArea/{controller=Home}/{action=Index}");


//////////////
Default Views folder rename http://jackhiston.com/2017/10/24/extending-the-razor-view-engine-with-view-location-expanders/
View -> ViewsNew CustomViewLocations.cs registered in statup.cs => services.Configure<RazorViewEngineOptions>( options => options.ViewLocationExpanders.Add( new CustomViewLocation()));





//////////////
//Configs

//////////////
//startup.cs
custom default MVC Area location folder in API/Areas in startup.cs rerouted through RazorViewEngineOptions Autofac container registration added AutoMapper service added, one coniguration two types of initialization - static and instance API AutoFact to Automapper registration added AutofacServiceProvider returned from ConfigureServices SignalR use and hub routing added Authentication registration: Authentication EF db context Identity core for user managment Added authentication with cookies Added conditional registration for SQL, SQLlite and InMemmory databases Condition is enum class from appsettings.json

ConfigureAutofacDbContexts
multiple SQL DBs in one project possible with Dummy Repository clones per DB scope
registration of connection string to context to Repository to service with IRepository

//////////////
//Program.cs
Added http instead of https routing for Fiddler test to: .UseUrls("http://localhost:5002")


//////////////
//webpack for webpack conf
webpack.config.js
//custom webpack to run from gulp
webpack.custom.js
//gulp default and webpack via gulp 
gulpfile.js


//////////////
//OrderContext DB migrations
dotnet ef migrations add CreateIdentitySchema --context TestContext dotnet ef database update --context TestContext


//////////////
//npm
npx webpack


//////////////
// decomposition->
API: WebApi, Controllers Infrastructure: ORMs contexts : [ EF ]; Repo and UOW realizations; Application logic: [ Checkers ]; Repo: {
  contains EF
  context; EF
  repo uses
  DAL concrete
  classes
} 
UOW: {
  Contains IRepository<ConcreteRealization>,
  maps DAL
  to BLL,
  returns View
  models
} Domain: Entity interfaces and Models For layers : [ DAL, BLL, View ]; IRepo,IUOW interfaces;

//////////////
// Folder structure
API:

Domain:
	DomainSpecific:
		domainName:
			domainNameAPI.cs; domainNameDAL.cs; IdomainNameService.cs; domainNameService.cs;
			
	domainManager.cs

	Universal:
		Entitiess.cs
		IEntities.cs
		IRepository.cs
		Iservice.cs
		IDomainManager.cs

Infrastructure:
	EF:
		DomainName:
			domainContext.cs
			domainRepository.cs
	
	IRepoEF.cs
	RepoEF.cs
	ServiceEF.cs
	
	Inmemory:
	
	IO:
		Logging:
		Serialization:
		Settings:
	
	SignalR:
	
	
//////////////
// layers relation directions
API->Infrastructure API->Domain Infrastructure->Domain 

TODO: [

	
	
] 

BACKLOG/MILESTONES: [

] 

DONE: [

	newOrder :
	[

		<- 15.07.2019 1h -> new order model
		<- 16.07.2019 1h -> new order migration
		<- 16.07.2019 1h -> new order migration

		<- 17.07.2019 45m -> new order migration cleanup

		<- 22.07.2019 1h -> new order currenciesDAL model

		<- 23.07.2019 1h -> new order currenciesDAL model

		<- 24.07.2019 1h -> new order currencies manager

		<- 25.07.2019 1h 30m -> currencies manager API resp

		<- 26.09.2019 1h 30m -> inmemmory, SQL, SQL, SQLlite conditional contexts

		<- 19.11.2019 3h 30m -> Autofac multiple Irepositories registration

		<- 29.03.2020 3h -> Ilogger, Iserializer, variables classes with variable.json

	]
	
	TMPL
	[
		
		<- done 10.04.2020 - 02.05.2020 p32h f15h in 4d -> refactor SB template to fire up state		
		[
			<- done p 10.04.2020 21:37 p8h->  create new TMPL from newOrder GIT branch with all infrastructure stuff
				double repositories, loggers, mappers
			<- done p 10.04.2020 21:37 p24h -> recreate All main project branches			
				p8h Blogging (manyToMany tags and CQRS)
				p8h NewOrder (volume recount)
				p8h CrossCurrencies (curency to curency throught curency)
			
			<- done 11.04.2020 4h-> refactor AllInOneModels			
			<- done 12.04.2020 4h -> refactor NewOrderModelsOneFile			
			-> CostControlModels
				G:\disk\Files\git\Core\crmvcsb\crmvcsb\Domain\TestModels\Models\CostControl\CostControlModels.cs
			
			<- 10.04.2020  4h -> TMPL branch create and cleanup, warmup
			<- 11.04.2020  3h45m -> TMPL namespaces refactor
			<- 12.04.2020  50m -> TMPL Blogging namespaces refactor
			<- 01.05.2020 2h30m -> 
				registering multiple domainContexts for domainRepositories; 
				several domainServices to one domainManager; 
				and reinnitializing domain DBs (newOrer and Currencies) from controller Index;
			<- done 02.05.2020 3h30m -> move blogging to brunch, 
				merge TMPL to master, merge blogging to master
			<- 08.11.2020 4h -> migrate from net core 2.0 to 3.0
		]
		~19h in 5days
	]
	
	~31h 50m in 15days

]