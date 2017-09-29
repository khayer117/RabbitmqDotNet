using System.Reflection;
using Autofac;
using Module = Autofac.Module;


namespace RabbitmqProducer
{
    public class ServiceRegistration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TestProducer>().AsSelf().AsImplementedInterfaces();

        }

    }
}
