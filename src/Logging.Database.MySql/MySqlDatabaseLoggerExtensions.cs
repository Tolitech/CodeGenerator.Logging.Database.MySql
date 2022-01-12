using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace Tolitech.CodeGenerator.Logging.Database.MySql
{
    public static class MySqlDatabaseLoggerExtensions
    {
        static public ILoggingBuilder AddMySqlDatabaseLogger(this ILoggingBuilder builder)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, MySqlDatabaseLoggerProvider>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IOptionsChangeTokenSource<DatabaseLoggerOptions>, LoggerProviderOptionsChangeTokenSource<DatabaseLoggerOptions, MySqlDatabaseLoggerProvider>>());
            return DatabaseLoggerExtensions.AddDatabaseLogger(builder);
        }

        static public ILoggingBuilder AddMySqlDatabaseLogger(this ILoggingBuilder builder, Action<DatabaseLoggerOptions> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.AddMySqlDatabaseLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
