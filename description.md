
//////////////
//webpack for webpack conf
webpack.config.js
//custom webpack to run from gulp
webpack.custom.js
//gulp default and webpack via gulp 
gulpfile.js


//////////////
//OrderContext DB migrations
dotnet ef migrations add CreateIdentitySchema --context OrderContext
dotnet ef database update --context OrderContext


//////////////
//npm
npx webpack


//////////////
//DDD decomposition->
    API:
        WebApi, Controllers
        
    Infrastructure:
        ORMs contexts : [EF];
        Repo and UOW realizations;
        Application logic:[Checkers];

        Repo:{
            contains EF context;
            EF repo uses DAL concrete classes
        }

        UOW:{
            Contains IRepository<ConcreteRealization>, maps DAL to BLL, returns View models
        }

    Domain:
        Entity interfaces and Models For layers :[DAL,BLL,View];
        IRepo,IUOW interfaces;

//////////////
//DDD layers relation directions
    API->Infrastructure
    API->Domain
    Infrastructure->Domain


TODO:[

    
]

BACKLOG:[
      
    
]

DONE:[

    <- done 13.06.2019 5h06m -> Ef core Orders model, migration and seed Many-to-many 
    <- done 14.06.2019 1h20m -> cqrs add and multiple context Autofac resolve
    <- done 14.06.2019 40m -> order created
    <- done 15.06.2019 40m -> order accounter interfaces
    <- done 15.06.2019 1h10m -> order new API, BLL interfaces
    <- done 15.06.2019 1h5m -> order deliverer, clenup

    ~36h in 9d

]