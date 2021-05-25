using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
// ReSharper disable ClassNeverInstantiated.Global

namespace TaskWebApi.Repository.Dapper
{
    public class ManagePersonDapper : Connection
    {
        public static async Task<Person> GetPerson(string id)
        {
            var exist = await IdExistAsync(id);

            await using var conn = new MySqlConnection(ConnStr);
            var reader = await conn.ExecuteReaderAsync("SELECT * FROM persons_tbl WHERE PersonId = @a", new { @a = id });
            var person = new Person();
            while (await reader.ReadAsync())
            {
                person.PersonId = id;
                person.Fname = reader.GetValue(1).ToString();
                person.Lname = reader.GetValue(2).ToString();
                person.Gender = reader.GetValue(3).ToString();
                person.PrivateNumber = reader.GetValue(4).ToString();
                person.Date = reader.GetValue(5).ToString();
                person.PhoneNumber = reader.GetValue(6).ToString();
                person.City = reader.GetValue(7).ToString();
                person.ImageLocation = reader.GetValue(8).ToString();
            }
            return exist ? person : null;
        }
        public static async Task<bool> DeletePersonAsync(string id)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            
            try
            {
                await conn.ExecuteAsync("DELETE FROM persons_tbl WHERE PersonId = @a", new {@a = id});
                await conn.CloseAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static async Task<bool> InsertPerson(Person person)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.ExecuteAsync("INSERT INTO persons_tbl(`Fname`, `Lname`, `Gender`, `PrivateNumber`, `Date`, `City`, `PhoneNumber`, `image`, `PersonId`) VALUES(@a,@b,@c,@d,@e,@f,@g,@h,@i)", 
                new {a = person.Fname, b = person.Lname, c = person.Gender, d = person.PrivateNumber, e = person.Date, f = person.City,g = person.PhoneNumber,
                    h = person.ImageLocation, i = person.PersonId});
            
            return true;
        }
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
        // public static async Task<bool> UpdatePerson(Person person)
        // {
        //     await using var conn = new MySqlConnection(ConnStr);
        //     await conn.OpenAsync();
        //     const string sql = "UPDATE `persons_tbl` SET fname = @b,lname=@c, PhoneNumber=@d, Image=@e,PrivateNumber = @f,Date=@g,Gender=@h WHERE PersonId = @a";
        //     await conn.ExecuteAsync(sql, new
        //     {
        //         a = person.PersonId, b = person.Fname, c = person.Lname, d = person.PhoneNumber, e = person.ImageLocation,
        //         f = person.PrivateNumber, g = person.Date, h = person.Gender
        //     });
        //     await conn.CloseAsync();
        //
        //     return true;
        // }
        public static async Task<ErrorClass> UpdatePersonSingle(string sql)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            await conn.ExecuteAsync(sql);
            
            return new ErrorClass{ErrorCode = ErrorList.OK, Description = $"{sql}"};
        }
    }
}