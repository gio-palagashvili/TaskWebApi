using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TaskWebApi.Repository.Dapper;

namespace TaskWeb.Repository
{
    public class DeletePersonRep : DeletePersonDapper
    {
        public DeletePersonRep(string s)
        {
            var z = DeletePersonAsync(s);
        }
    }
}