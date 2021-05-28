using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using TaskWebApi;
using TaskWebApi.Repository.Dapper;

// ReSharper disable ClassNeverInstantiated.Global

namespace TaskWeb.Repository
{
    public class ManagePerson : Connection
    {
        private static string GenerateRandomId()
        {
            var random = new Random();
            return random.Next(10000, 1000000000).ToString();
        }
        private static string RandomId { get; set; }
        private static bool IdExist()
        {
            using var conn = new MySqlConnection(ConnStr);
            conn.Open();
            const string command = "SELECT * FROM persons_tbl WHERE PersonId = @A";
            var persons = conn.Query<Person>(command, new { A = RandomId }).ToList();
            return (persons.Count != 0);
        }

        public static async Task<bool> IdExistAsyncRoute(string id)
        {
        // todo doesn't work
            const string connStr = "server=localhost;user=root;database=taskweb_db;port=3306;password=''";
            await using var conn = new MySqlConnection(connStr);
            await conn.OpenAsync();
            const string sql = "SELECT * FROM persons_tbl WHERE PersonId = @A";
            var z = (List<Person>) await conn.QueryAsync<Person>(sql, new {a = id});
            
            return z.Count > 0;
        }
        public static async Task<List<Person>> FilterPerson(string value)
        {
            return await ManagePersonDapper.FilterPerson(value);
        }
        public static async Task InsertPersonRep(Person person)
        {
            RandomId = GenerateRandomId();
            var check = IdExist();
            while (check)
            {
                RandomId = GenerateRandomId();
                check = IdExist();
            }
            await ManagePersonDapper.InsertPerson(new Person
            {
                PersonId = RandomId,
                City = person.City,
                Fname = person.Fname,
                Lname = person.Lname,
                PhoneNumber = person.PhoneNumber,
                PrivateNumber = person.PrivateNumber,
                Gender = person.Gender,
                ImageLocation = person.ImageLocation,
                Date = person.Date
            });
        }

        public static async Task<Person> GetPerson(string id)
        {
            var person = await ManagePersonDapper.GetPerson(id);
            return person;
        }
        public static async Task<bool> DeletePerson(string id)
        {
            var z = ManagePersonDapper.DeletePersonAsync(id);
            return await z;
        }
        public static async Task<ErrorClass> UpdatePerson(UpdateClass update)
        {
            if (!await IdExistAsyncRoute(update.PersonId))
            {
                return new ErrorClass
                {
                    ErrorCode = ErrorList.ERROR_NON_EXISTENT,
                    Description = $"person that you are trying to update doesn't exist (submitted id : {update.PersonId})"
                };
            }
            var person = await ManagePersonDapper.GetPerson(update.PersonId);

            var allowedTypes = new List<string>
                {"Fname", "Lname", "Gender", "PrivateNumber", "Date", "City", "PhoneNumber", "Image"};
            if (!allowedTypes.Contains(update.Column))
            {
                return new ErrorClass
                {
                    ErrorCode = ErrorList.ERROR_INVALID_INPUT,
                    Description = $"(case sensitive) Fname, Lname , Gender, PrivateNumber, Date, PhoneNumber, Image(submitted value : {update.Value})"
                };
            }

            var sql = $"UPDATE persons_tbl SET {update.Column} = '{update.Value}' WHERE PersonId = {update.PersonId}";
            switch (update.Column)
            {
                case "Fname" or "Lname":
                    if (await NameExists(person.Fname, update.Value)) return new ErrorClass { ErrorCode = ErrorList.OK, Description = "name already exists" };
                    break;
                case "gender":
                    if (update.Value is not "0" or "1")
                        return new ErrorClass
                        { ErrorCode = ErrorList.ERROR_INVALID_INPUT, Description = "gender must be 1 or 0" };
                    break;
                case "PrivateNumber":
                    var v = PersonVerify.PrivateNumber(update.Value);
                    if (v.ErrorCode != ErrorList.OK) return v;
                    break;
                case "Date":
                    var d = PersonVerify.Date(Convert.ToDateTime(update.Value));
                    if (d.ErrorCode != ErrorList.OK) return d;
                    break;
                case "City":
                    if (Regex.IsMatch(update.Column, "\\d"))
                        return new ErrorClass
                        { ErrorCode = ErrorList.ERROR_INVALID_INPUT, Description = "city contains numbers" };
                    break;
            }
            await ManagePersonDapper.UpdatePersonSingle(sql);

            return new ErrorClass { ErrorCode = ErrorList.OK, Description = "User Updated" };
        }
    }
}