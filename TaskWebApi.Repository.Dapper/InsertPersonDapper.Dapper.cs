using System;
using System.Data.SqlClient;
using Dapper;
using MySql.Data.MySqlClient;
using TaskWebApi.Repository;
    
namespace TaskWebApi.Repository.Dapper
{
    public class InsertPersonDapper
    {
        protected const string ConnStr = "server=localhost;user=root;database=taskweb_db;port=3306;password=''";
        // ReSharper disable once MemberCanBeProtected.Global
        public InsertPersonDapper()
        {
        }
        // ReSharper disable once MemberCanBeProtected.Global
        public static bool Insert(Person person)
        {
            using var conn = new MySqlConnection(ConnStr);
            conn.Execute("INSERT INTO persons_tbl(`Fname`, `Lname`, `Gender`, `PrivateNumber`, `Date`, `City`, `PhoneNumber`, `PersonId`) VALUES(@a,@b,@c,@d,@e,@f,@g,@h)", 
                new {a = person.Fname,b = person.Lname, c = person.Gender,d = person.PrivateNumber,e = person.Date,f = person.City,g = person.PhoneNumber,h = person.PersonId});
            return true;
        }
    }
}