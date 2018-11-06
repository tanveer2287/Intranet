using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Core.Repository;
using OrgWebAPI.Repository;

namespace OrgWebAPI.AutofacModules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Reader<>)).As(typeof(IReader<>));
            builder.RegisterGeneric(typeof(Writer<>)).As(typeof(IWriter<>));
        }
    }
}
