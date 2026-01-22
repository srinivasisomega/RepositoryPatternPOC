using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    
        public class SqlCommandLoggingInterceptor : DbCommandInterceptor
        {
            private readonly ILogger<SqlCommandLoggingInterceptor> _logger;
            private const int SlowQueryThresholdMs = 500; 

            public SqlCommandLoggingInterceptor(ILogger<SqlCommandLoggingInterceptor> logger)
            {
                _logger = logger;
            }

            public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
            {
                LogCommand(command, eventData);
                return base.ReaderExecuted(command, eventData, result);
            }

            public override int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
            {
                LogCommand(command, eventData);
                return base.NonQueryExecuted(command, eventData, result);
            }

            public override object? ScalarExecuted(DbCommand command, CommandExecutedEventData eventData, object? result)
            {
                LogCommand(command, eventData);
                return base.ScalarExecuted(command, eventData, result);
            }

            public override ValueTask<DbDataReader> ReaderExecutedAsync(
                DbCommand command,
                CommandExecutedEventData eventData,
                DbDataReader result,
                CancellationToken cancellationToken = default)
            {
                LogCommand(command, eventData);
                return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
            }

            public override ValueTask<int> NonQueryExecutedAsync(
                DbCommand command,
                CommandExecutedEventData eventData,
                int result,
                CancellationToken cancellationToken = default)
            {
                LogCommand(command, eventData);
                return base.NonQueryExecutedAsync(command, eventData, result, cancellationToken);
            }

            public override ValueTask<object?> ScalarExecutedAsync(
                DbCommand command,
                CommandExecutedEventData eventData,
                object? result,
                CancellationToken cancellationToken = default)
            {
                LogCommand(command, eventData);
                return base.ScalarExecutedAsync(command, eventData, result, cancellationToken);
            }

            private void LogCommand(DbCommand command, CommandExecutedEventData eventData)
            {
                var elapsedMs = eventData.Duration.TotalMilliseconds;
                var sql = command.CommandText;

                if (elapsedMs > SlowQueryThresholdMs)
                {
                    _logger.LogWarning("SLOW SQL ({Duration} ms)\n{CommandText}", elapsedMs, sql);
                }
                else
                {
                    _logger.LogInformation("SQL ({Duration} ms)\n{CommandText}", elapsedMs, sql);
                }
            }
        }
}
