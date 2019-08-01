using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Mvc.Razor;

using crmvcsb.Domain.NewOrder;
namespace crmvcsb
{

    using Autofac;
    using Autofac.Extensions.DependencyInjection;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;

    using crmvcsb.Infrastructure.SignalR;

    using crmvcsb.Infrastructure.EF;
    using crmvcsb.Domain.TestModels;
    using crmvcsb.Domain.Interfaces;

    using crmvcsb.Infrastructure.EF.newOrder;
    using crmvcsb.Infrastructure.EF.costControl;

    public class Startup
    {
        public IContainer ApplicationContainer { get; private set; }

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

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


            services.AddDbContext<TestContext>(o =>
            o.UseSqlServer(
                Configuration.GetConnectionString("LocalDbConnection")));

            services.AddDbContext<NewOrderContext>(o =>
            o.UseSqlServer(
                Configuration.GetConnectionString("LocalNewOrderConnection")));

            services.AddDbContext<CostControllContext>(o =>
            o.UseSqlServer(Configuration.GetConnectionString("CostControlDb")));

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

            AutofacServiceProvider r=null;

            try
            {
                this.ApplicationContainer = autofacContainer.Build();
                r = new AutofacServiceProvider(this.ApplicationContainer);
            }
            catch (Exception e)
            {

            }
            return r;
        }


        public ContainerBuilder ConfigureAutofac(IServiceCollection services, ContainerBuilder autofacContainer)
        {

            /**EF, repo and UOW reg */
            autofacContainer.RegisterType<TestContext>()
                .As<DbContext>().WithMetadata("Name", "TestContext")
                .InstancePerLifetimeScope();

            //autofacContainer.RegisterType<TestContext>().AsImplementedInterfaces();

            autofacContainer.RegisterType<NewOrderContext>()
                .As<DbContext>().WithMetadata("Name", "NewOrderContext")
                .InstancePerLifetimeScope();

            autofacContainer.RegisterType<CostControllContext>()
             .As<DbContext>().WithMetadata("Name", "CostControllContext")
             .InstancePerLifetimeScope();



            autofacContainer.RegisterType<RepositoryEF>()
                .As<IRepository>()
                .InstancePerLifetimeScope();



            autofacContainer.RegisterType<NewOrdermanager>()
                .As<INewOrdermanager>()
                .InstancePerLifetimeScope();



            //*DAL->BLL reg */
            autofacContainer.RegisterType<BlogEF>()
                .As<IBlogEF>().InstancePerLifetimeScope();
            autofacContainer.RegisterType<BlogBLL>()
                .As<IBlogBLL>().InstancePerLifetimeScope();
            autofacContainer.RegisterType<PostBLL>()
                .As<IPostBLL>().InstancePerLifetimeScope();

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
            });
        }

        public IConfiguration GetConfig()
        {
            return this.Configuration;
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
