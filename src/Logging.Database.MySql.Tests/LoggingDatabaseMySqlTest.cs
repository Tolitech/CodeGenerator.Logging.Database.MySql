using System;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Xunit;

namespace Tolitech.CodeGenerator.Logging.Database.MySql.Tests
{
    public class LoggingDatabaseMySqlTest
    {
        private const string CONNECTION_STRING = "Server=localhost;Port=3306;Database=Logging;User=root;Password=Password@123;";

        private readonly ILogger<LoggingDatabaseMySqlTest> _logger;

        public LoggingDatabaseMySqlTest()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var logLevel = (LogLevel)config.GetSection("Logging:Database:LogLevel").GetValue(typeof(LogLevel), "Default");

            var loggerFactory = LoggerFactory.Create(logger =>
            {
                logger
                    .AddConfiguration(config.GetSection("Logging"))
                    .AddMySqlDatabaseLogger(x =>
                    {
                        x.LogLevel = logLevel;
                        x.ConnectionString = CONNECTION_STRING;
                    });
            });

            _logger = loggerFactory.CreateLogger<LoggingDatabaseMySqlTest>();
        }

        [Fact(DisplayName = "LoggingDatabaseMySql - Log - Valid")]
        public void LoggingDatabaseMySql_LogEntry_Valid()
        {
            _logger.LogTrace("Trace");
            _logger.LogDebug("Debug");
            _logger.LogInformation("Information");
            _logger.LogWarning("Warning");
            _logger.LogError("Error");
            _logger.LogCritical("Critical");
            Thread.Sleep(5000);

            string selectCount = "select count(*) from Cg_Log";
            using var conn = new MySqlConnection(CONNECTION_STRING);
            using var command = new MySqlCommand(selectCount, conn);
            conn.Open();
            long count = (long)command.ExecuteScalar();
            conn.Close();

            Assert.True(count > 0);
        }
    }
}