

namespace crmvcsb
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Microsoft.AspNetCore.Mvc.Razor;


    using Autofac;
    using Autofac.Extensions.DependencyInjection;

    using AutoMapper;

    using crmvcsb.Infrastructure.SignalR;

    using Microsoft.EntityFrameworkCore;

    using crmvcsb.Infrastructure.EF;
    using crmvcsb.Domain.Universal.IRepository;


    using crmvcsb.Domain.DomainSpecific.Blogging.API;
    using crmvcsb.Domain.DomainSpecific.Blogging.BLL;
    using crmvcsb.Infrastructure.EF.Blogging;
    using crmvcsb.Infrastructure.EF.Currencies;
    using crmvcsb.Domain.DomainSpecific.Currency.DAL;
    using crmvcsb.Domain.DomainSpecific.Currency.API;

    using crmvcsb.Infrastructure.EF.NewOrder;


    using crmvcsb.Domain.Universal;
    using order.Domain.Interfaces;
    using order.Infrastructure.EF;

    using order.Domain.Models;
    using order.Domain.Services;

    /*Build in logging*/
    using Microsoft.Extensions.Logging;

    using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;

        enum ContextType
    {
        SQL, SQLLite, InMemmory
    }
    class RegistrationSettings
    {
        public ContextType ContextType { get; set; }
    }

    public class Startup
    {
        /*Build in logging*/
        private static ILogger _logger;

        public IContainer ApplicationContainer { get; private set; }

        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            /*Build in logging to console message*/
            crmvcsb.Infrastructure.IO.Settings.MessagesInitialization.Init();

            var Message = crmvcsb.Infrastructure.IO.Settings.MessagesInitialization.Variables.Messages.SrviceMessages.TestMessage;
            _logger?.LogInformation("Message displayed: {Message}", Message);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            /** Register route to move Areas default MVC folder to custom location */
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("API/Areas/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("API/Areas/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });

      
            services.AddMvc(options => options.EnableEndpointRouting = false);
            
            /*SignalR registration*/
            services.AddSignalR();

            /*Autofac autofacContainer */
            var autofacContainer = new ContainerBuilder();


            /*Automapper Register */
            services.AddAutoMapper(typeof(Startup));

            /*Mapper initialize with Instance API initialization */
            var config = ConfigureAutoMapper();
            IMapper mapper = new Mapper(config);


            /*Autofac registrations */
            autofacContainer.Populate(services);

            /*Registration of automapper with autofac Instance API */
            autofacContainer.RegisterInstance(mapper).As<IMapper>();

            /* Chose registration of test sql lite or sql server*/

            if (Configuration.GetSection("RegistrationSettings").Get<RegistrationSettings>().ContextType == ContextType.SQL)
            {
                ConfigureAutofacDbContexts(services, autofacContainer);
            }
            if (Configuration.GetSection("RegistrationSettings").Get<RegistrationSettings>().ContextType == ContextType.SQLLite)
            {
                ConfigureSqlLiteDbContexts(services, autofacContainer);
            }
            if (Configuration.GetSection("RegistrationSettings").Get<RegistrationSettings>().ContextType == ContextType.InMemmory)
            {
                ConfigureInMemmoryDbContexts(services, autofacContainer);
            }


            ConfigureAutofac(services, autofacContainer);
          

            AutofacServiceProvider r = null;

            try
            {
                this.ApplicationContainer = autofacContainer.Build();
                r = new AutofacServiceProvider(this.ApplicationContainer);
            }
            catch (Exception)
            {
                throw;
            }

            try
            {

                // services.AddDbContext<TestContext>(o =>
                // o.UseSqlServer(Configuration.GetConnectionString("LocalDbConnection")));

                // services.AddDbContext<NewOrderContext>(o =>
                // o.UseSqlServer(Configuration.GetConnectionString("LocalNewOrderConnection")));

                // services.AddDbContext<CostControllContext>(o =>
                // o.UseSqlServer(Configuration.GetConnectionString("CostControlDb")));

			}
			catch (Exception e)
            {

            }
   
            return r;
        }

        /* Db context registration with sql server for connection strings from appsettings.json */
        public ContainerBuilder ConfigureAutofacDbContexts(IServiceCollection services, ContainerBuilder autofacContainer)
        {
  
            /*
            * Registering multiple IRepository clones with different connections trings
            * For multiple SQL DBs in one project
            */

          

            //--------
            autofacContainer.RegisterType<RepositoryCurrency>()
            .WithParameter("context",

                new CurrencyContext(new DbContextOptionsBuilder<CurrencyContext>()
                .UseSqlServer(Configuration.GetConnectionString("LocalCurrenciesConnection")).Options))

            .As<IRepositoryEF>().AsSelf()
            .InstancePerLifetimeScope();


           
            autofacContainer.RegisterType<BloggingRepository>()
            .WithParameter("context",

                new BloggingContext(new DbContextOptionsBuilder<BloggingContext>()
                .UseSqlServer(Configuration.GetConnectionString("LocalBloggingConnection")).Options))

            .As<IRepositoryEF>().AsSelf()
            .InstancePerLifetimeScope();


            /*Register repository dummy clones for DB scope to services*/
            autofacContainer.Register(ctx => new ServiceEF(ctx.Resolve<RepositoryEF>(), ctx.Resolve<IMapper>()))
            .As<IServiceEF>()
            .InstancePerLifetimeScope();

            return autofacContainer;
        }

        /* Db context configuration for test with SqlLite database */
        public ContainerBuilder ConfigureSqlLiteDbContexts(IServiceCollection services, ContainerBuilder autofacContainer)
        {
     

            /**EF, repo and UOW reg */
   
            autofacContainer.RegisterType<ContextNewOrder>()
                .As<DbContext>()
                .WithParameter("options", new DbContextOptionsBuilder<ContextNewOrder>()
                    .UseSqlite("Data Source=app.db").Options)
                .WithMetadata("Name", "NewOrderContext")
                .InstancePerLifetimeScope();
     
            /**EF context , and repo registration */       
            autofacContainer.RegisterType<ContextNewOrder>()
                .As<DbContext>().WithMetadata("Name", "OrderRepo")
                .InstancePerLifetimeScope();

            autofacContainer.RegisterType<RepositoryEF>()
                .As<IRepository>()
                .WithMetadata<AppendMetadata>(m => m.For(am => am.AppendName, "NewOrderContext"))
                .InstancePerLifetimeScope();
         

            return autofacContainer;
        }

        /* Db context configuration for test with InMemmory database */
        public ContainerBuilder ConfigureInMemmoryDbContexts(IServiceCollection services, ContainerBuilder autofacContainer)
        {            

       
          
            return autofacContainer;
        }
        

        public ContainerBuilder ConfigureAutofac(IServiceCollection services, ContainerBuilder autofacContainer)
        {
            //*DAL->BLL reg */
          

            autofacContainer.RegisterType<OrdersManagerWrite>()
                .As<IOrdersManagerWrite>()
                .WithParameter(new TypedParameter(typeof(RepositoryEF),
                    new RepositoryEF(
                        new OrderContext(new DbContextOptionsBuilder<OrderContext>()
                            .UseSqlServer(Configuration.GetConnectionString("LocalNewOrderConnection")).Options)
                    )
                ))
                .InstancePerLifetimeScope();

            autofacContainer.RegisterType<BirdAccounter>()
                .As<IBirdAccounter>().InstancePerLifetimeScope();
            autofacContainer.RegisterType<TortiseAccounter>()
                .As<ITortiseAccounter>().InstancePerLifetimeScope();

            
            autofacContainer.RegisterType<OrderDeliveryBirdBLL>()
                .As<IOrderDeliveryBirdBLL>().InstancePerLifetimeScope();
            autofacContainer.RegisterType<OrderBLL>()
                .As<IOrderBLL>().InstancePerLifetimeScope();
            autofacContainer.RegisterType<OrderDeliveryTortiseBLL>()
                .As<IOrderDeliveryTortiseBLL>().InstancePerLifetimeScope();
            
            autofacContainer.RegisterType<OrderDeliveryBirdAPI>()
                .As<IOrderDeliveryBirdAPI>().InstancePerLifetimeScope();
            autofacContainer.RegisterType<OrderCreateAPI>()
                .As<IOrderCreateAPI>().InstancePerLifetimeScope();
            autofacContainer.RegisterType<OrderDeliveryTortiseAPI>()
                .As<IOrderDeliveryTortiseAPI>().InstancePerLifetimeScope();       

            autofacContainer.RegisterType<DimensionalUnitAPI>()
                .As<IDimensionalUnitAPI>().InstancePerLifetimeScope();
        

            return autofacContainer;
        }

        public MapperConfiguration ConfigureAutoMapper()
        {
            return new MapperConfiguration(cfg =>
            {
               

                cfg.CreateMap<CurrencyRatesDAL, CrossCurrenciesAPI>()
                    .ForMember(d => d.From, m => m.MapFrom(src => src.CurrencyFrom.Name))
                    .ForMember(d => d.To, m => m.MapFrom(src => src.CurrencyTo.Name))
                    .ReverseMap().ForAllMembers(o => o.Ignore());

                cfg.CreateMap<PostEF, PostBLL>(MemberList.None).ReverseMap();

                cfg.CreateMap<PersonAdsPostCommand, PostEF>()
                    .ForMember(dest => dest.AuthorId, m => m.MapFrom(src => src.PersonId));

                cfg.CreateMap<AddPostAPI, PostEF>()
                    .ForMember(dest => dest.AuthorId, m => m.MapFrom(src => src.PersonId))
                    .ForMember(dest => dest.BlogId, m => m.MapFrom(src => src.BlogId));

                cfg.CreateMap<PersonEF, PersonAPI>();
                cfg.CreateMap<BlogEF, BlogAPI>();
                cfg.CreateMap<PostEF, PostAPI>().ReverseMap();
           
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            try
            {
          
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "areas",
                   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });
            }
            catch (Exception e)
            {

            }

            /* must be added after use mvc */
            app.UseSignalR(routes =>
            {
                routes.MapHub<SignalRhub>("/rHub");
                routes.MapHub<SignalRWorks>("/rWork");
            });
        }

        public IConfiguration GetConfig()
        {
            return this.Configuration;
        }
    }


    public class AppendMetadata {
        public string AppendName {get; set;}
    }

    public class CustomViewLocation : IViewLocationExpander
    {
        string ValueKey = "Views";
        public IEnumerable<string> ExpandViewLocations(
            ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {

            return ExpandViewLocationsCore(viewLocations);
        }

        private IEnumerable<string> ExpandViewLocationsCore(IEnumerable<string> viewLocations)
        {
            viewLocations = viewLocations.Select(s => s.Replace("Views", ValueKey));

            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values[ValueKey] = context.ActionContext.RouteData.Values[ValueKey]?.ToString();
        }
    }

}
