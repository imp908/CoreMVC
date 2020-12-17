namespace crmvcsb.Infrastructure.IoC
{
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using crmvcsb.Infrastructure.Validation;
    using crmvcsb.Universal.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using crmvcsb.Infrastructure.IO.Logging;
    using crmvcsb.Universal.Infrastructure;
    using crmvcsb.Infrastructure.IO.Serialization;

    /*Build in logging*/
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

            autofacContainer.RegisterType<ValidatorCustom>().As<IValidatorCustom>();
            autofacContainer.RegisterType<LoggerCustom>().As<ILoggerCustom>();
            autofacContainer.RegisterType<JSONio>().As<ISerialization>();

            return autofacContainer;
        }

        public static ContainerBuilder GetContainer()
        {
            return autofacContainer;
        }
    }
}
