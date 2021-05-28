using System;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace TaskWebApi.Repository.Dapper
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RequestLoggerDappper : Connection
    {
        public static async Task LogDapper(RequestLog log)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            const string sql = "INSERT INTO requestlog_tbl(`Method`, `Path`, `StatusCode`,`Host`,`Headers`) VALUES(@Method,@Path,@StatusCode,@Host,@Headers)";
            await conn.ExecuteAsync(sql,log);
        }
    }
}