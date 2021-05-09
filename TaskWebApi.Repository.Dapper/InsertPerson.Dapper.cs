using System;
using System.Data.SqlClient;
using Dapper;
using MySql.Data.MySqlClient;
using TaskWebApi.Repository;
// ReSharper disable once MemberCanBeProtected.Global
// ReSharper disable once MemberCanBeProtected.Global

namespace TaskWebApi.Repository.Dapper
{
    public class InsertPersonDapper
    {
        protected const string ConnStr = "server=localhost;user=root;database=taskweb_db;port=3306;password=''";
        public InsertPersonDapper()
        {
        }

        protected static bool Insert(Person person)
        {
            using var conn = new MySqlConnection(ConnStr);
            conn.Execute("INSERT INTO persons_tbl(`Fname`, `Lname`, `Gender`, `PrivateNumber`, `Date`, `City`, `PhoneNumber`, `image`, `PersonId`) VALUES(@a,@b,@c,@d,@e,@f,@g,@h,@i)", 
                new {a = person.Fname, b = person.Lname, c = person.Gender, d = person.PrivateNumber, e = person.Date, f = person.City,g = person.PhoneNumber,
                    h = person.ImageLocation, i = person.PersonId});
            Console.WriteLine("done");
            return true;
        }
    }
}