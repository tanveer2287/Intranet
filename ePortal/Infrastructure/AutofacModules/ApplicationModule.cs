using Autofac;
using Services;
using Services.Interrface;
using Core.Helpers;
using Microsoft.Extensions.Configuration;

namespace WebAppUI.Infrastructure.AutofacModules
{
    public class ApplicationModule :Module
    {
        private readonly IConfigurationRoot _configuration;

        public ApplicationModule(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration).As<IConfiguration>().SingleInstance();
            builder.RegisterType<ApiHttpClient>();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>();

        }
    }
}
