using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using RabbitmqDotNetCore.Infrastructure;
using RabbitmqDotNetCore.Rabbitmq;
using Serilog;
using Module = Autofac.Module;

namespace RabbitmqDotNetCore
{
    public class ServiceRegistration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = AssembliesProvider.Instance.Assemblies.ToArray();

            RegisterCommon(builder, assemblies);
            RegisterLogger(builder);
            RegisterMediator(builder, assemblies);
            RegisterCommandDispather(builder, assemblies);
            RegisterRabbitmq(builder);
        }

        private static void RegisterCommon(ContainerBuilder builder, Assembly[] assemblies)
        {
            builder.Register<Func<Type, object>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();
                return ctx.Resolve;
            });
        }
        private static void RegisterRabbitmq(ContainerBuilder builder)
        {
            builder.RegisterType<RabbitmqProducerService>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<RabbitmqConsumerService>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<RabbitmqConnect>().AsSelf().AsImplementedInterfaces();
        }
        private static void RegisterMediator(ContainerBuilder builder, Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies)
                .AsSelf()
                .AsClosedTypesOf(typeof(ICommandHandler<,>))
                .AsImplementedInterfaces();

            builder.RegisterType<CommandBus>().AsSelf().AsImplementedInterfaces();

        }
        private static void RegisterCommandDispather(ContainerBuilder builder, Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies)
                .AsSelf()
                .AsClosedTypesOf(typeof(IActionCommandHandler<>))
                .AsImplementedInterfaces();

            builder.RegisterType<ActionCommandDispacher>().AsSelf().AsImplementedInterfaces();

        }
        private void RegisterLogger(ContainerBuilder builder)
        {
            const string MessageTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} " +
                                           "{MachineName}->{ProcessId}->{ThreadId} " +
                                           "[{Level}] " +
                                           "{Message}{NewLine}{Exception}";

            var logFormat = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs") +
                Path.DirectorySeparatorChar + "{Date}.txt";

            var seriLogger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .WriteTo.RollingFile(logFormat, outputTemplate: MessageTemplate)
                .WriteTo.Console(outputTemplate: MessageTemplate)
                .Enrich.FromLogContext()
                .CreateLogger();


            builder.RegisterInstance(seriLogger);

            builder.RegisterType<Logger>().AsSelf().AsImplementedInterfaces();

            Log.Logger = seriLogger;
        }


    }
}
