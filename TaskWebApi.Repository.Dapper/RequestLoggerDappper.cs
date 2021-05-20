using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace TaskWebApi.Repository.Dapper
{
    public class RequestLoggerDappper : Connection
    {
        public static async Task<RequestLog> LogDapper(RequestLog log)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            const string sql = "INSERT INTO requestlog_tbl(`Method`, `Path`, `StatusCode`) VALUES(@Method,@Path,@StatusCode)";
            await conn.ExecuteAsync(sql,log);
            return log;
        }
    }
}