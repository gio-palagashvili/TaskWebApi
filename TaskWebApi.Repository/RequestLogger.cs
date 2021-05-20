using System.Threading.Tasks;
using TaskWebApi;
using TaskWebApi.Repository.Dapper;
// ReSharper disable ClassNeverInstantiated.Global

namespace TaskWeb.Repository
{
    public class RequestLogger
    {
        public static async Task Log(RequestLog log)
        {
            await RequestLoggerDappper.LogDapper(log);
        }
    }
}