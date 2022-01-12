using System;
using System.Data.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace Tolitech.CodeGenerator.Logging.Database.MySql
{
    [ProviderAlias("Database")]
    public class MySqlDatabaseLoggerProvider : DatabaseLoggerProvider
    {
        public MySqlDatabaseLoggerProvider(IOptionsMonitor<DatabaseLoggerOptions> settings) : base(settings.CurrentValue)
        {

        }

        public MySqlDatabaseLoggerProvider(DatabaseLoggerOptions settings) : base(settings)
        {

        }

        protected override DbConnection GetNewConnection
        {
            get
            {
                return new MySqlConnection(Settings.ConnectionString);
            }
        }

        protected override string Sql
        {
            get
            {
                return @"insert into `Cg_Log`
                    (`LogId`, `Time`, `UserName`, `HostName`, `Category`, `Level`, `Text`, `Exception`, `EventId`, `ActivityId`, `UserId`, `LoginName`, `ActionId`, `ActionName`, `RequestId`, `RequestPath`, `FilePath`, `Sql`, `Parameters`, `StateText`, `StateProperties`, `ScopeText`, `ScopeProperties`) 
                    values 
                    (@logId, @time, @userName, @hostName, @category, @level, @text, @exception, @eventId, @activityId, @userId, @loginName, @actionId, @actionName, @requestId, @requestPath, @filePath, @sql, @parameters, @stateText, @stateProperties, @scopeText, @scopeProperties)";
            }
        }
    }
}
