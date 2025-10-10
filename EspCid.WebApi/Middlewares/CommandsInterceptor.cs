using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EspCid.WebApi.Middlewares;

public class CommandsInterceptor: DbCommandInterceptor
{
    public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData,
        InterceptionResult<DbDataReader> result)
    {
        if (command.CommandText.Length > 10000)
            Console.WriteLine("Command text too long");
        return base.ReaderExecuting(command, eventData, result);
    }
}