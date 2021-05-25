using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;

namespace TaskWebApi.Repository.Dapper
{
    public class Connection
    {
        protected const string ConnStr = "server=localhost;user=root;database=taskweb_db;port=3306;password=''";
        protected static bool IdExist(string id)
        {
            using var conn = new MySqlConnection(ConnStr);
            conn.Open();
            const string command = "SELECT * FROM persons_tbl WHERE PersonId = @A";
            var persons = conn.Query<Person>(command,new {A = id}).ToList();
            
            return (persons.Count > 0);
        }
        protected static async Task<bool> IdExistAsync(string id)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            var persons = (List<PersonRelations>)await conn.QueryAsync<PersonRelations>("SELECT * FROM persons_tbl WHERE PersonId = @A",new {A = id});
            await conn.CloseAsync();
            
            return persons.Count > 0;
        }
        protected static async Task<bool> IdHasRelation(string id)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            var persons = (List<PersonRelations>)await conn.QueryAsync<PersonRelations>("SELECT * FROM relations_tbl WHERE PersonId = @A OR RelationId = @A",new {A = id});
            await conn.CloseAsync();
            
            return persons.Count > 0;
        }
        protected static async Task<bool> NameExists(string lastName, string name)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            var x = await conn.QueryAsync<Person>("SELECT * FROM persons_tbl WHERE Fname = @a AND Lname = @b", new {a = name, b = lastName});
            await conn.CloseAsync();
            return x.Any();
        }
        protected static async Task<bool> RelationExists(SingleRelation relation)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            
            var relationsList = (List<PersonRelations>)await conn.QueryAsync<PersonRelations>("SELECT * FROM relations_tbl WHERE PersonId = @a AND RelationId = @b",new {a = relation.PersonId, b = relation.RelationId});
            await conn.CloseAsync();
            
            return relationsList.Any();
        } 
    }
}