using System.Collections.Generic;
using System.Threading.Tasks;
using TaskWebApi;
using TaskWebApi.Repository.Dapper;
// ReSharper disable MemberCanBeMadeStatic.Global
// ReSharper disable CA1822

namespace TaskWeb.Repository
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PersonRelationship
    {
        public static async Task<List<PersonRelations>> GetRelations(string id)
        {
            return await PersonRelationshipDapper.GetRelationDapper(id);
        }
        public static async Task<ErrorClass> DeleteRelationsAll(string id)
        {
            return await PersonRelationshipDapper.DeleteAllRelations(id);
        }
        public static async Task<ErrorClass> DeleteRelation(SingleRelation relation)
        {
            return await PersonRelationshipDapper.DeleteRelation(relation);
        }

        public static async Task<ErrorClass> CreateRelation(SingleRelation relation)
        {
            return await PersonRelationshipDapper.CreateRelation(relation);
        }
    }
}