using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

// ReSharper disable ClassNeverInstantiated.Global

namespace TaskWebApi.Repository.Dapper
{
    public class PersonRelationshipDapper : Connection
    {
        public static async Task<List<PersonRelations>> GetRelationDapper(string id)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            const string sql = "SELECT * FROM relations_tbl WHERE PersonId = @a OR RelationId = @a";
        
            return (List<PersonRelations>) await conn.QueryAsync<PersonRelations>(sql,new {@a = id});
        }

        public static async Task<bool> DeleteAllRelations(string id)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();

            return true;
        }
    }
}