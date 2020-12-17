
namespace crmvcsb
{

    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using AutoMapper;
    using crmvcsb.Infrastructure.EF;
    using crmvcsb.Infrastructure.EF.Currencies;
    using crmvcsb.Infrastructure.EF.NewOrder;
    using crmvcsb.Infrastructure.IoC;
    using crmvcsb.Infrastructure.Mapping;
    using crmvcsb.Infrastructure.SignalR;
    using crmvcsb.Universal;
    using crmvcsb.Universal.DomainSpecific.Currency;
    using crmvcsb.Universal.DomainSpecific.NewOrder;
    using crmvcsb.Universal.Infrastructure;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    /*Build in logging*/
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
        private static Microsoft.Extensions.Logging.ILogger _logger;

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
            //var autofacContainer = new ContainerBuilder();
            
            /*Automapper Register */
            services.AddAutoMapper(typeof(Startup));

            /*Mapper initialize with Instance API initialization */
            var config = ConfigureAutoMapper();
            IMapper mapper = new Mapper(config);

            /*Autofac registrations */
            //autofacContainer.Populate(services);
            AutofacConfig.config(services);

            /*Registration of automapper with autofac Instance API */
            //autofacContainer.RegisterInstance(mapper).As<IMapper>();
            AutofacConfig.GetContainer().RegisterInstance(mapper).As<IMapper>();

            /* Chose registration of test sql lite or sql server*/

            AutofacConfig.ConfigureAutofac(services);

            if (Configuration.GetSection("RegistrationSettings").Get<RegistrationSettings>().ContextType == ContextType.SQL)
            {
                ConfigureAutofacDbContexts(services, AutofacConfig.GetContainer());
            }
            if (Configuration.GetSection("RegistrationSettings").Get<RegistrationSettings>().ContextType == ContextType.SQLLite)
            {
                ConfigureSqlLiteDbContexts(services, AutofacConfig.GetContainer());
            }
            if (Configuration.GetSection("RegistrationSettings").Get<RegistrationSettings>().ContextType == ContextType.InMemmory)
            {
                ConfigureInMemmoryDbContexts(services, AutofacConfig.GetContainer());
            }

                  

            AutofacServiceProvider r = null;

            ConfigureFluentValidation(services);

            try
            {
                this.ApplicationContainer = AutofacConfig.GetContainer().Build();
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
			catch (Exception)
            {
                throw;
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



            ///------------------
            /////Keyed registration not work - write only

            //autofacContainer.RegisterType<RepositoryNewOrderRead>()
            //.WithParameter("context",

            //    new ContextNewOrderRead(new DbContextOptionsBuilder<ContextNewOrderRead>()
            //    .UseSqlServer(Configuration.GetConnectionString("LocalNewOrderConnection")).Options)

            //)
            //.As<IRepositoryEF>().AsSelf()
            //.InstancePerLifetimeScope();
            //autofacContainer.RegisterType<RepositoryNewOrderWrite>()
            //.WithParameter("context",

            //    new ContextNewOrderWrite(new DbContextOptionsBuilder<ContextNewOrderWrite>()
            //    .UseSqlServer(Configuration.GetConnectionString("LocalNewOrderConnectionWrite")).Options)

            //).As<IRepositoryEF>().AsSelf()
            //.InstancePerLifetimeScope();


            
            ////--------
            autofacContainer.RegisterType<RepositoryNewOrderRead>()
            .WithParameter("context",
                new ContextNewOrderRead(new DbContextOptionsBuilder<ContextNewOrderRead>()
                .UseSqlServer(Configuration.GetConnectionString("LocalNewOrderConnection")).Options))
            .As<IRepositoryEFRead>().AsSelf()
            .InstancePerLifetimeScope();
            autofacContainer.RegisterType<RepositoryNewOrderWrite>()
            .WithParameter("context",
                new ContextNewOrderWrite(new DbContextOptionsBuilder<ContextNewOrderWrite>()
                .UseSqlServer(Configuration.GetConnectionString("LocalNewOrderConnectionWrite")).Options))
            .As<IRepositoryEFWrite>().AsSelf()
            .InstancePerLifetimeScope();
            autofacContainer.RegisterType<RepositoryCurrencyRead>()
            .WithParameter("context",
                new CurrencyContextRead(new DbContextOptionsBuilder<CurrencyContextRead>()
                .UseSqlServer(Configuration.GetConnectionString("LocalCurrenciesConnection")).Options))
            .As<IRepositoryEFRead>().AsSelf()
            .InstancePerLifetimeScope();
            autofacContainer.RegisterType<RepositoryCurrencyWrite>()
            .WithParameter("context",
                new CurrencyContextWrite(new DbContextOptionsBuilder<CurrencyContextWrite>()
                .UseSqlServer(Configuration.GetConnectionString("LocalCurrenciesConnectionWrite")).Options))
            .As<IRepositoryEFWrite>().AsSelf()
            .InstancePerLifetimeScope();

            autofacContainer.Register(ctx => new NewOrderServiceEF(
                ctx.Resolve<RepositoryNewOrderRead>(),
                ctx.Resolve<RepositoryNewOrderWrite>(),
                ctx.Resolve<IMapper>(),
                ctx.Resolve<IValidatorCustom>()))
            .As<INewOrderServiceEF>()
            .AsSelf()
            .InstancePerLifetimeScope();

            autofacContainer.Register(ctx => new CurrencyServiceEF(
                ctx.Resolve<RepositoryCurrencyRead>(),
                ctx.Resolve<RepositoryCurrencyWrite>(),
                ctx.Resolve<IMapper>(),
                ctx.Resolve<IValidatorCustom>()))
            .As<ICurrencyServiceEF>()
            .AsSelf()
            .InstancePerLifetimeScope();

            //PropertyAccessMode registration of Iservice to Manager to static
            //autofacContainer.Register(c =>
            //{
            //    var result = new NewOrderManager();
            //    var dep = c.Resolve<INewOrderServiceEF>();
            //    var dep2 = c.Resolve<ICurrencyServiceEF>();
            //    result.BindService(dep, dep2);
            //    return result;
            //})
            //.As<NewOrderManager>();





            ///------------------
            /////Keyed registration not work - write only

            //autofacContainer.Register(c=> 
            //new RepositoryNewOrderRead(new ContextNewOrderRead(new DbContextOptionsBuilder<ContextNewOrderRead>()
            //    .UseSqlServer(Configuration.GetConnectionString("LocalNewOrderConnection")).Options)))
            //    .As<IRepositoryEF>()
            //    .Keyed<IRepositoryEF>("orederRead")
            //    .InstancePerLifetimeScope();
            //autofacContainer.Register(c =>
            //new RepositoryNewOrderWrite(new ContextNewOrderWrite(new DbContextOptionsBuilder<ContextNewOrderWrite>()
            //   .UseSqlServer(Configuration.GetConnectionString("LocalNewOrderConnectionWrite")).Options)))
            //   .As<IRepositoryEF>()
            //   .Keyed<IRepositoryEF>("orederWrite")
            //   .InstancePerLifetimeScope();
            //autofacContainer.Register(c =>
            //new RepositoryCurrencyRead(new CurrencyContextRead(new DbContextOptionsBuilder<CurrencyContextRead>()
            //    .UseSqlServer(Configuration.GetConnectionString("LocalCurrenciesConnection")).Options)))
            //    .As<IRepositoryEF>()
            //    .Keyed<IRepositoryEF>("currencyRead")
            //    .InstancePerLifetimeScope();
            //autofacContainer.Register(c =>
            //new RepositoryCurrencyWrite(new CurrencyContextWrite(new DbContextOptionsBuilder<CurrencyContextWrite>()
            //   .UseSqlServer(Configuration.GetConnectionString("LocalCurrenciesConnectionWrite")).Options)))
            //   .As<IRepositoryEF>()
            //   .Keyed<IRepositoryEF>("currencyWrite")
            //   .InstancePerLifetimeScope();

            //autofacContainer.RegisterType<NewOrderServiceEF>()
            //    .WithParameter(new ResolvedParameter(
            //           (pi, ctx) => pi.ParameterType == typeof(IRepositoryEF),
            //           (pi, ctx) => ctx.ResolveKeyed<IRepositoryEF>("orederRead")))
            //    .WithParameter(new ResolvedParameter(
            //           (pi, ctx) => pi.ParameterType == typeof(IRepositoryEF),
            //           (pi, ctx) => ctx.ResolveKeyed<IRepositoryEF>("orederWrite")))
            //    .WithParameter(new TypedParameter(typeof(IMapper), "mapper"))
            //    .WithParameter(new TypedParameter(typeof(IValidatorCustom), "validator"));

            //autofacContainer.RegisterType<CurrencyServiceEF>()
            //    .WithParameter(new ResolvedParameter(
            //           (pi, ctx) => pi.ParameterType == typeof(IRepositoryEF),
            //           (pi, ctx) => ctx.ResolveKeyed<IRepositoryEF>("currencyRead")))
            //    .WithParameter(new ResolvedParameter(
            //           (pi, ctx) => pi.ParameterType == typeof(IRepositoryEF),
            //           (pi, ctx) => ctx.ResolveKeyed<IRepositoryEF>("currencyWrite")))
            //    .WithParameter(new TypedParameter(typeof(IMapper), "mapper"))
            //    .WithParameter(new TypedParameter(typeof(IValidatorCustom), "validator"));



            autofacContainer.Register(ctx => new NewOrderManager(
                ctx.Resolve<NewOrderServiceEF>()
                , ctx.Resolve<CurrencyServiceEF>()
                ))
                .As<INewOrderManager>()
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

            autofacContainer.RegisterType<RepositoryEF>()
                .As<IRepository>()
                .WithMetadata<AppendMetadata>(m => m.For(am => am.AppendName, "NewOrderContext"))
                .InstancePerLifetimeScope();

            autofacContainer.RegisterType<NewOrderServiceEF>()
                .As<INewOrderServiceEF>()
                .WithMetadata<AppendMetadata>(m => m.For(am => am.AppendName, "NewOrderContext"))
                .InstancePerLifetimeScope();

            return autofacContainer;
        }

        /* Db context configuration for test with InMemmory database */
        public ContainerBuilder ConfigureInMemmoryDbContexts(IServiceCollection services, ContainerBuilder autofacContainer)
        {            

            autofacContainer.RegisterType<ContextNewOrder>()
                .As<DbContext>()
                .WithParameter("options", new DbContextOptionsBuilder<ContextNewOrder>()
                    .UseInMemoryDatabase("NewOrderContext").Options)
                .WithMetadata("Name", "NewOrderContext")
                .InstancePerLifetimeScope();

            autofacContainer.RegisterType<RepositoryEF>()
                .As<IRepository>()
                .WithMetadata<AppendMetadata>(m => m.For(am => am.AppendName, "NewOrderContext"))
                .InstancePerLifetimeScope();

            autofacContainer.RegisterType<NewOrderServiceEF>()
                .As<INewOrderServiceEF>()
                .WithMetadata<AppendMetadata>(m => m.For(am => am.AppendName, "NewOrderContext"))
                .InstancePerLifetimeScope();

            return autofacContainer;
        }
        



        public MapperConfiguration ConfigureAutoMapper()
        {
            return CurrenciesMapping.config();
        }

        public void ConfigureFluentValidation(IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation();
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
