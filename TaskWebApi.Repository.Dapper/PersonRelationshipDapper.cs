﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            if (await IdHasRelation(id) == false)
            {
                return new ErrorClass
                    {ErrorCode = ErrorList.ERROR_NON_EXISTENT, Description = "person doesn't have relationships"};
            }
            await conn.ExecuteAsync("DELETE FROM relations_tbl WHERE PersonId = @a OR RelationId = @a", new {a = id});
            await conn.CloseAsync();
            return new ErrorClass{ErrorCode = ErrorList.OK, Description = "relations Deleted"};
        }
        public static async Task<ErrorClass> DeleteRelation(SingleRelation relation)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            const string sql = "DELETE FROM relations_tbl WHERE PersonId = @a AND RelationId = @b";
           if(await RelationExists(relation) == false) return new ErrorClass
               {ErrorCode = ErrorList.ERROR_NON_EXISTENT, Description = "no relationship was found"};
           
           await conn.ExecuteAsync(sql, new { a = relation.PersonId, b = relation.RelationId});
         
            return new ErrorClass {ErrorCode = ErrorList.OK, Description = "successfully Deleted"};
        }
        public static async Task<ErrorClass> CreateRelation(SingleRelation relation)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            const string sql = "INSERT INTO relations_tbl(`PersonId`, `RelationId`, `RelationType`) VALUES(@a,@b,@c)";

            if (await RelationExists(relation))
            {
                return new ErrorClass
                    {ErrorCode = ErrorList.ERROR_DUPLICATE, Description = "relationship already exists"};
            }
            if (await IdExistAsync(relation.PersonId) == false)
            {
                return new ErrorClass
                    {ErrorCode = ErrorList.ERROR_NON_EXISTENT, Description = $"user : {relation.PersonId} doesn't exist"};
            }
            if (await IdExistAsync(relation.RelationId) == false)
            {
                return new ErrorClass
                    {ErrorCode = ErrorList.ERROR_NON_EXISTENT, Description = $"user : {relation.RelationId} doesn't exist"};
            }

            if (relation.PersonId == relation.RelationId) return new ErrorClass { ErrorCode = ErrorList.ERROR_INVALID_INPUT, Description = "both ids are the same"};
            
            var allowedRelations = new[] {"acquaintance", "relative", "colleague","other"};
            if (relation.RelationType != allowedRelations[0] && relation.RelationType != allowedRelations[1] && 
                relation.RelationType != allowedRelations[2] && relation.RelationType != allowedRelations[3])
            {
                return new ErrorClass
                {
                    ErrorCode = ErrorList.ERROR_INVALID_INPUT,
                    Description = "only acquaintance, relative, colleague, 'other' keywords are allowed"
                };
            }
            await conn.ExecuteAsync(sql,new {a = relation.PersonId, b = relation.RelationId, c = relation.RelationType});
            await conn.CloseAsync();

            return new ErrorClass {ErrorCode = ErrorList.OK, Description = "relation created"};
        }

        public static async Task<List<PersonRelations>> RelationReportDapper(string id, string type)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            const string sql = "SELECT * FROM relations_tbl WHERE relationType = @b AND PersonId = @a OR RelationId = @a ";
            var z = await conn.QueryAsync<PersonRelations>(sql, new {a = id, b = type});
            return (List<PersonRelations>) z;
        }
    }
}