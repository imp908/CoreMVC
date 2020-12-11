using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmvcsb.Infrastructure.IoC
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

    using Microsoft.EntityFrameworkCore;

    using crmvcsb.Infrastructure.SignalR;

    using crmvcsb.Infrastructure.EF;
    using crmvcsb.Universal;

    using crmvcsb.Infrastructure.EF.NewOrder;

    using crmvcsb.Universal.DomainSpecific.NewOrder;

    using crmvcsb.Infrastructure.EF.Currencies;
    using crmvcsb.Universal.DomainSpecific.Currency;
    using crmvcsb.Universal.DomainSpecific.Currency.API;
    using crmvcsb.Universal.DomainSpecific.Currency.DAL;


    using crmvcsb.Infrastructure.Mapping;

    /*Build in logging*/
    using Microsoft.Extensions.Logging;

    using FluentValidation.AspNetCore;

    using Autofac;
    public static class AutofacConfig
    {
        static ContainerBuilder autofacContainer;
        public static void config(IServiceCollection services)
        {
            /*Autofac autofacContainer */
            autofacContainer = new ContainerBuilder();

            /*Autofac registrations */
            autofacContainer.Populate(services);
           
        }
        public static ContainerBuilder ConfigureAutofac(IServiceCollection services)
        {
            //*DAL->BLL reg */

            return autofacContainer;
        }

        public static ContainerBuilder GetContainer()
        {
            return autofacContainer;
        }
    }
}
