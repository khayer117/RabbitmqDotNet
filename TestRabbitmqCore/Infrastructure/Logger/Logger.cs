
namespace RabbitmqDotNetCore.Infrastructure
{
    using System;

    using Serilog;
    using Serilog.Context;
    using Serilog.Events;
    using System.Collections.Generic;
    
    //using SeriLogger = Serilog.ILogger;
    using SeriLogger = Serilog.Core.Logger;
    
    public class Logger : ILogger
    {
        private readonly SeriLogger logger;

        public Logger(SeriLogger logger)
        {
            this.logger = logger;

        }

        public void Debug(string message, params object[] args)
        {
            this.logger.Debug(message, args);
        }

        public void Info(string message, params object[] args)
        {
            this.logger.Information(message, args);
        }

        public void Warn(string message, params object[] args)
        {
            this.logger.Warning(message, args);
        }

        public void Error(string message, params object[] args)
        {
            this.logger.Error(message, args);
        }

        public void Error(Exception e, string message, params object[] args)
        {
            this.logger.Error(e, message, args);
        }

        public void Fatal(string message, params object[] args)
        {
            this.logger.Fatal(message, args);
        }

        public void Fatal(Exception e, string message, params object[] args)
        {
            this.logger.Fatal(e, message, args);
        }

        public IDisposable WithProperty(string name, object value)
        {
            return LogContext.PushProperty(name, value);
        }

    }

    public class DummyDisposable: IDisposable
    {
        public void Dispose()
        {
            
        }
    }
}