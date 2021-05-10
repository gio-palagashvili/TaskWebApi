using System;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace TaskWebApi.Repository.Dapper
{
    public class DeletePersonDapper : Connection
    {
        protected static async Task<bool> DeletePersonAsync(string id)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            await conn.ExecuteAsync("DELETE FROM persons_tbl WHERE PersonId = @a", new {@a = id});
            await conn.CloseAsync();
            return true;
        }
    }
}