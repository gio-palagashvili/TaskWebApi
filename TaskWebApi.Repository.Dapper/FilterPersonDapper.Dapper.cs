using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
// ReSharper disable CollectionNeverQueried.Local

// ReSharper disable ClassNeverInstantiated.Global

namespace TaskWebApi.Repository.Dapper
{
    public class FilterPersonDapper : Connection
    {
        public static async Task<List<Person>> FilterPerson(string value)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            const string sql = "SELECT * FROM persons_tbl WHERE Lname LIKE @a OR Fname LIKE @a OR Lname LIKE @a OR PrivateNumber LIKE @a OR PhoneNumber LIKE @a OR PersonId LIKE @a";
            var persons = new List<Person>();
            var z = await conn.QueryAsync<Person>(sql, new {@a = $"%{value}%"});
            if (!z.Any())
            {
                return new List<Person>();
            }
            
            return (List<Person>) z;
        }
    }
}