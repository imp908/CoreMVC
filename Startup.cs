using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Mvc.Razor;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using AutoMapper;

using fileshare.Infrastructure.SignalR;

using Microsoft.AspNetCore.Authentication.Cookies;

namespace mvccoresb
{
    using mvccoresb.Domain.Interfaces;
    using mvccoresb.Domain.TestModels;
    using chat.Domain.Models;

    using mvccoresb.Infrastructure.EF;
    using Microsoft.EntityFrameworkCore;
    
    using Microsoft.AspNetCore.Identity;

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

            /*Authentication provider */
            services.AddDbContext<IdentityContext>(o => 
                o.UseSqlServer(Configuration.GetConnectionString("LocalAuthConnection")));
            
            // установка конфигурации подключения
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Identity/Account/Register");
                });

            services.AddMvc();

            /** Renames all defalt Views including Areas/Area/Views folders to custom name */
            services.Configure<RazorViewEngineOptions>(
                options => options.ViewLocationExpanders.Add(
            new CustomViewLocation()));

            services.AddDbContext<TestContext>(o => 
                o.UseSqlServer(Configuration.GetConnectionString("LocalDbConnection")));
                  

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
            ConfigureAutofac(services,autofacContainer);

            /*Registration of automapper with autofac Instance API */
            autofacContainer.RegisterInstance(mapper).As<IMapper>();    

            this.ApplicationContainer = autofacContainer.Build();
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        public ContainerBuilder ConfigureAutofac(IServiceCollection services,ContainerBuilder autofacContainer)
        {            
          
            /**EF,repo and UOW reg */
            autofacContainer.RegisterType<TestContext>().As<DbContext>()
                .InstancePerLifetimeScope();

            autofacContainer.RegisterType<RepositoryEF>()
                .As<IRepository>().InstancePerLifetimeScope();

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

            return autofacContainer;
        }

        public MapperConfiguration ConfigureAutoMapper(){
            return new MapperConfiguration(cfg => {
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
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
 
            app.UseAuthentication();

            app.UseSignalR(routes =>{
                routes.MapHub<SignalRhub>("/rHub");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                /**mapping for default scaffolded area */
                routes.MapRoute(
                    name: "defaultWithArea",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


                /** mapping for custom testarea */
                routes.MapAreaRoute(
                    name: "TestArea",
                    areaName: "TestArea",
                    template: "TestArea/{controller=Home}/{action=Index}"
                );

                 routes.MapAreaRoute(
                    name: "IdentityArea",
                    areaName: "Identity",
                    template: "Identity/{controller=Home}/{action=Index}"
                );
            });
        }
    }

    public static class AutoMapperStaticConfiguration
    {
        public static void Configure(){
            /*Mapper initialize with Static initialization*/
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BlogEF, BlogBLL>();
                cfg.CreateMap<PostEF, PostBLL>();                
            });
        }
    }
}
