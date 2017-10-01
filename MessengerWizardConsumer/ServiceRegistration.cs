using System.Reflection;
using Autofac;
using MessengerWizardConsumer;
using Module = Autofac.Module;


namespace RabbitmqProducer
{
    public class MessengerWizardConsumer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TestMWDataSync>().AsSelf().AsImplementedInterfaces();

        }

    }
}
