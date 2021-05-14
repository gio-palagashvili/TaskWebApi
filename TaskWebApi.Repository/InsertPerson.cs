using System;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using TaskWebApi;
using TaskWebApi.Repository.Dapper;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace TaskWeb.Repository
{
    public class InsertPersonRep : InsertPersonDapper
    {
        private static string GenerateRandomId()
        {
            var random = new Random();
            return random.Next(10000,1000000000).ToString();
        }
        private bool IdExist()
        {
            using var conn = new MySqlConnection(ConnStr);
            conn.Open();
            const string command = "SELECT * FROM persons_tbl WHERE PersonId = @A";
            var persons = conn.Query<Person>(command,new {A = RandomId}).ToList();
            return (persons.Count != 0);
        }
        private string RandomId { get; set; }
        public InsertPersonRep(Person person)
        {
            RandomId = GenerateRandomId();
            var check = IdExist();
            while (check)
            {
                RandomId = GenerateRandomId();
                check = IdExist();
            }
   
            Insert(new Person() {PersonId = RandomId, City = person.City, Fname = person.Fname, Lname = person.Lname, PhoneNumber = person.PhoneNumber,
                PrivateNumber = person.PrivateNumber,Gender = person.Gender,ImageLocation = person.ImageLocation, Date = person.Date});
        }
    }
}