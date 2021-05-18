using System.Collections.Generic;
using System.Threading.Tasks;
using TaskWebApi;
using TaskWebApi.Repository.Dapper;

namespace TaskWeb.Repository
{
    public static class FilterPerson
    {
        public static async Task<List<Person>> FilterRep(string value)
        {
            return await FilterPersonDapper.FilterPerson(value);
        }
    }
   
}