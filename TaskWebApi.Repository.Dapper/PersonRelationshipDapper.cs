﻿using System.Collections.Generic;
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

        public static async Task<ErrorClass> DeleteAllRelations(string id)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            if (await IdExistAsync(id) == false)
            {
                return new ErrorClass
                    {ErrorCode = ErrorList.ERROR_NON_EXISTENT, Description = "person doesn't have relationships"};
            }
            await conn.ExecuteAsync("DELETE FROM relations_tbl WHERE PersonId = @a OR RelationId = @a", new {a = id});
            
            return new ErrorClass{ErrorCode = ErrorList.OK, Description = "relations Deleted"};
        }
    }
}