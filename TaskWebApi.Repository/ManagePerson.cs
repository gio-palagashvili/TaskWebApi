using System;
using System.Collections.Generic;
using System.Linq;
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
            var persons = conn.Query<Person>(command, new {A = RandomId}).ToList();
            return (persons.Count != 0);
        }
        public static async Task<bool> IdExistAsyncRoute(string id)
        {
            return await IdExistAsync(id);
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
            
            await ManagePersonDapper.InsertPerson(new Person()
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

        public static async Task<bool> UpdatePerson(Person person)
        {
            //todo verify data check
            return await ManagePersonDapper.UpdatePerson(person);
        }
     
    }
}