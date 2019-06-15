using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Mvc.Razor;


namespace mvccoresb
{

    using Autofac;
    using Autofac.Extensions.DependencyInjection;

    using AutoMapper;

    using mvccoresb.Infrastructure.SignalR;

    using Microsoft.EntityFrameworkCore;

    using mvccoresb.Infrastructure.EF;
    using mvccoresb.Domain.TestModels;

    using order.Domain.Services;
    using order.Domain.Models;
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


            services.AddDbContext<OrderContext>(o =>
            o.UseSqlServer(
                Configuration.GetConnectionString("LocalOrderConnection")));

            services.AddMvc();

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
     
            /**EF context , and repo registration */       
            autofacContainer.RegisterType<OrderContext>()
                .As<DbContext>().WithMetadata("Name", "OrderRepo")
                .InstancePerLifetimeScope();

            autofacContainer.RegisterType<RepositoryEF>()
                .As<IRepository>()                
                .InstancePerLifetimeScope();

      
            /*Orders registration */
            autofacContainer.RegisterType<Deliverer>()
                .As<IDeliverer>().InstancePerLifetimeScope();

            //*DAL->BLL reg */
            autofacContainer.RegisterType<BlogEF>()
                .As<IBlogEF>().InstancePerLifetimeScope();
            autofacContainer.RegisterType<BlogBLL>()
                .As<IBlogBLL>().InstancePerLifetimeScope();
            autofacContainer.RegisterType<PostBLL>()
                .As<IPostBLL>().InstancePerLifetimeScope();          
            autofacContainer.RegisterType<OrdersManagerWrite>()
                .As<IOrdersManagerWrite>()
                .WithParameter(new TypedParameter(typeof(RepositoryEF),
                    new RepositoryEF(
                        new OrderContext(new DbContextOptionsBuilder<OrderContext>()
                            .UseSqlServer(Configuration.GetConnectionString("LocalOrderConnection")).Options)
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

                                                       
                cfg.CreateMap<OrderItemDAL, OrderItemUpdateDAL>();
                
                cfg.CreateMap<OrderItemDAL, OrderBLL>().ForMember(dest => dest.AddressFrom,m => m.MapFrom(src => src.Direction.AddressFrom)).ReverseMap();
                cfg.CreateMap<OrderItemDAL, OrderBLL>().ForMember(dest => dest.AddressTo, m => m.MapFrom(src => src.Direction.AddressTo)).ReverseMap();
                cfg.CreateMap<OrderItemDAL, OrderBLL>().ForMember(dest => dest.OrderId, m => m.MapFrom(src => src.Id)).ReverseMap();
                
                cfg.CreateMap<OrderDeliveryBirdBLL, OrderUpdateBLL>();
                cfg.CreateMap<OrderUpdateBLL, OrderItemDAL>();

                cfg.CreateMap<OrderDeliveryTortiseBLL, OrderUpdateBLL>();
                cfg.CreateMap<OrderItemDAL, OrderDeliveryTortiseAPI>().ForMember(dest => dest.DeliveryDate, m => m.MapFrom(
                    src => (System.DateTime.Now.AddDays(src.DaysToDelivery!=null ? (double)src.DaysToDelivery : 0)
                )));

                cfg.CreateMap<OrderDeliveryBirdBLL, OrderDeliveryBirdAPI>();
                cfg.CreateMap<OrderDeliveryTortiseBLL, OrderDeliveryTortiseAPI>();
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
