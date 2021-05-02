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
            
            Console.WriteLine("image location");
            localPerson.ImageLocation = Console.ReadLine();
            localPerson.ImageLocation = CommandCheck.FileLocation(localPerson.ImageLocation,RandomId);
            
            Console.WriteLine("Date of birth (yyyy/mm/dd) must be over or equal to 18");
            localPerson.Date = Console.ReadLine();
            localPerson.Date = CommandCheck.Date(DateTime.Parse(localPerson.Date!));
            if (localPerson.Date == "underage") Environment.Exit(1);
            
            Console.WriteLine("First name");
            localPerson.Fname = Console.ReadLine();
            localPerson.Fname = CommandCheck.Fname(localPerson.Fname);

            Console.WriteLine("Last name");
            localPerson.Lname = Console.ReadLine();
            localPerson.Lname = CommandCheck.Lname(localPerson.Lname);

            Console.WriteLine("city");
            localPerson.City = Console.ReadLine();
            localPerson.City = CommandCheck.City(localPerson.City);

            Console.WriteLine("gender (1 or 0 male and female)");
            localPerson.Gender = Console.ReadLine();
            localPerson.Gender = CommandCheck.Gender(localPerson.Gender);
            
            Console.WriteLine("private number");
            localPerson.PrivateNumber = Console.ReadLine();
            localPerson.PrivateNumber = CommandCheck.PrivateNumber(localPerson.PrivateNumber);
            
            Console.WriteLine("phone number");
            localPerson.PhoneNumber = Console.ReadLine();
            localPerson.PhoneNumber = CommandCheck.PhoneNumber(localPerson.PhoneNumber);

            Insert(new Person() {PersonId = RandomId, City = localPerson.City, Fname = localPerson.Fname, Lname = localPerson.Lname, PhoneNumber = localPerson.PhoneNumber,
                PrivateNumber = localPerson.PrivateNumber,Gender = localPerson.Gender,ImageLocation = localPerson.ImageLocation, Date = localPerson.Date});
        }
    }
}