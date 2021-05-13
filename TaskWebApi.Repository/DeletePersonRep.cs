using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TaskWebApi.Repository.Dapper;

namespace TaskWeb.Repository
{
    public class DeletePersonRep : DeletePersonDapper
    {
        public static async Task<bool> Delete(string s)
        {
            var z = DeletePersonAsync(s);
            return await z;
        }
    }
}