using TaskWebApi;
using TaskWebApi.Repository.Dapper;

namespace TaskWeb.Repository
{
    public class GetPersonRep : RetrievePersonDapper
    {
        public Person GetPersonRepp(string id)
        { 
            Person person = GetPerson(id);
            return person;
        }
    }
}
