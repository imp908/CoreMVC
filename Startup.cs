using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvccoresb.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Authentication.Cookies;

using Microsoft.AspNetCore.Mvc.Razor;


namespace mvccoresb
{

    using Autofac;
    using Autofac.Extensions.DependencyInjection;

    using AutoMapper;

    using mvccoresb.Infrastructure.SignalR;


    using mvccoresb.Domain.Interfaces;
    using mvccoresb.Domain.TestModels;
    using chat.Domain.Models;
    using mvccoresb.Infrastructure.EF;    

    using Microsoft.EntityFrameworkCore;

    using order.Domain.Models.Ordering;
    using order.Domain.Interfaces;
    using order.Infrastructure.EF;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IContainer ApplicationContainer { get; private set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
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

            /*Authentication authorization provider */
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("LocalAuthConnection")));
           
            services.AddMvc();

            /*Test db context */
            services.AddDbContext<TestContext>(o =>
               o.UseSqlServer(
                   Configuration.GetConnectionString("LocalDbConnection")));

            services.AddDbContext<OrderContext>(o =>
                          o.UseSqlServer(
                              Configuration.GetConnectionString("LocalOrderConnection")));

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
            ConfigureAutofac(services, autofacContainer);

            /*Registration of automapper with autofac Instance API */
            autofacContainer.RegisterInstance(mapper).As<IMapper>();

            try{
            this.ApplicationContainer = autofacContainer.Build();
            }catch(Exception e)
            {

            }
            return new AutofacServiceProvider(this.ApplicationContainer);
        }


        public ContainerBuilder ConfigureAutofac(IServiceCollection services, ContainerBuilder autofacContainer)
        {

            /**EF,repo and UOW reg */
            autofacContainer.RegisterType<TestContext>().As<DbContext>().WithMetadata("Name", "TestRepo")
                .InstancePerLifetimeScope();

            autofacContainer.RegisterType<OrderContext>().As<DbContext>().WithMetadata("Name", "OrderRepo")
                .InstancePerLifetimeScope();

            autofacContainer.RegisterType<RepositoryEF>()
                .As<IRepository>()                
                .InstancePerLifetimeScope();

            autofacContainer.RegisterType<CQRSBloggingWrite>()
                .As<ICQRSBloggingWrite>().InstancePerLifetimeScope();
            autofacContainer.RegisterType<CQRSBloggingRead>()
                .As<ICQRSBloggingRead>().InstancePerLifetimeScope();

            //*DAL->BLL reg */
            autofacContainer.RegisterType<BlogEF>()
                .As<IBlogEF>().InstancePerLifetimeScope();
            autofacContainer.RegisterType<BlogBLL>()
                .As<IBlogBLL>().InstancePerLifetimeScope();
            autofacContainer.RegisterType<PostBLL>()
                .As<IPostBLL>().InstancePerLifetimeScope();


            autofacContainer.RegisterType<DimensionalUnitAPI>()
                .As<IDimensionalUnitAPI>().InstancePerLifetimeScope();
            autofacContainer.RegisterType<OrderCreateAPI>()
                .As<IOrderCreateAPI>().InstancePerLifetimeScope();           
            autofacContainer.RegisterType<OrderItemAPI>()
                .As<IOrderItemAPI>().InstancePerLifetimeScope();
            autofacContainer.RegisterType<OrdersManagerWrite>()
                .As<IOrdersManagerWrite>()
                .WithParameter(new TypedParameter(typeof(RepositoryEF),
                    new RepositoryEF(
                        new OrderContext(new DbContextOptionsBuilder<OrderContext>()
                            .UseSqlServer(Configuration.GetConnectionString("LocalOrderConnection")).Options)
                    )                        
                ))
                .InstancePerLifetimeScope();

            return autofacContainer;
        }

        public MapperConfiguration ConfigureAutoMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                //cfg.AddProfiles(typeof(BlogEF), typeof(BlogBLL));
                cfg.CreateMap<BlogEF, BlogBLL>()
                    .ForMember(dest => dest.Id, m => m.MapFrom(src => src.BlogId))
                    .ForMember(dest => dest.Posts, m => m.Ignore());

                cfg.CreateMap<PostEF, PostBLL>(MemberList.None).ReverseMap();

                cfg.CreateMap<PersonAdsPostCommand, PostEF>()
                    .ForMember(dest => dest.AuthorId, m => m.MapFrom(src => src.PersonId));

                cfg.CreateMap<AddPostAPI, PostEF>()
                    .ForMember(dest => dest.AuthorId, m => m.MapFrom(src => src.PersonId))
                    .ForMember(dest => dest.BlogId, m => m.MapFrom(src => src.BlogId));

                cfg.CreateMap<PersonEF, PersonAPI>();
                cfg.CreateMap<BlogEF, BlogAPI>();
                cfg.CreateMap<PostEF, PostAPI>().ReverseMap();

                cfg.CreateMap<OrderItemDAL,OrderItemAPI>()
                    .ForMember(dest => dest.DaysToDelivery, m => m.MapFrom(src => src.DaysToDelivery))
                    .ForMember(dest => dest.DeliveryPrice, m => m.MapFrom(src => src.DeliveryPrice))
                ;

                cfg.CreateMap<OrderItemDAL, OrderItemUpdateDAL>();
                
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "areas",
                   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });

            /* must be added after use mvc */
            app.UseSignalR(routes =>
            {
                routes.MapHub<SignalRhub>("/rHub");
            });
        }
    }

    public static class AutoMapperStaticConfiguration
    {
        public static void Configure()
        {
            /*Mapper initialize with Static initialization*/
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BlogEF, BlogBLL>();
                cfg.CreateMap<PostEF, PostBLL>();
            });
        }
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
