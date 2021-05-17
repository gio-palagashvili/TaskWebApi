using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

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

        protected static async Task<bool> FnameExists(string name)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            var x = await conn.QueryAsync<Person>("SELECT * FROM persons_tbl WHERE Fname = @a", new {@a = name});
            await conn.CloseAsync();
            return x.Any();
        }
        protected static async Task<bool> LnameExists(string name)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            var x = await conn.QueryAsync<Person>("SELECT * FROM persons_tbl WHERE Lname = @a", new {@a = name});
            await conn.CloseAsync();
            
            return x.Any();
        }
    }
}