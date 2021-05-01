using System;
using System.Linq;
using System.Text.RegularExpressions;
using Dapper;
using MySql.Data.MySqlClient;
using TaskWebApi;
using TaskWebApi.Repository.Dapper;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace TaskWeb.Repository
{
    public class InsertPerson : InsertPersonDapper
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
        public InsertPerson()
        {
            RandomId = GenerateRandomId();
            var check = IdExist();
            while (check)
            {
                RandomId = GenerateRandomId();
                check = IdExist();
            }
            var localPerson = new Person();
            
            var command = new CommandCheck();

            Console.WriteLine("First name");
            localPerson.Fname = Console.ReadLine();
            localPerson.Fname = command.Fname(localPerson.Fname);

            Console.WriteLine("Last name");
            localPerson.Lname = Console.ReadLine();
            localPerson.Lname = command.Lname(localPerson.Lname);

            Console.WriteLine("city");
            localPerson.City = Console.ReadLine();
            localPerson.City = command.City(localPerson.City);

            Console.WriteLine("gender (1 or 0 male and female)");
            localPerson.Gender = Console.ReadLine();
            localPerson.Gender = command.Gender(localPerson.Gender);
            
            Console.WriteLine("private number");
            localPerson.PrivateNumber = Console.ReadLine();
            localPerson.PrivateNumber = command.PrivateNumber(localPerson.PrivateNumber);
            
            Console.WriteLine("phone number");
            localPerson.PhoneNumber = Console.ReadLine();
            localPerson.PhoneNumber = command.PhoneNumber(localPerson.PhoneNumber);

            Console.WriteLine("Date of birth (yyyy/mm/dd) must be over or equal to 18");
            localPerson.Date = Console.ReadLine();
            localPerson.Date = command.Date(DateTime.Parse(localPerson.Date!));

            Console.WriteLine("image location");
            localPerson.ImageLocation = Console.ReadLine();
            localPerson.ImageLocation = command.FileLocation(localPerson.ImageLocation);

            // Insert(new Person() {PersonId = RandomId,City = "city", Fname = "fname", Lname = "lname", PhoneNumber = "595595959",
            //     PrivateNumber = "00000000",Gender = "1",Date = "12/12/12"});
        }
    }
}