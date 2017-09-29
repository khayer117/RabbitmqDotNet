using Autofac;
using RabbitmqDotNetCore.Rabbitmq;

namespace RabbitmqDotNetCore
{
    public class ServiceRegistration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RabbitmqExchangeMessageService>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<RabbitmqDirectMessageService>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<RabbitmqConnect>().AsSelf().AsImplementedInterfaces();
        }

    }
}
