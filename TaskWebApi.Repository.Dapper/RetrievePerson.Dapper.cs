using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace TaskWebApi.Repository.Dapper
{
    public class RetrievePersonDapper : Connection
    {
        
        protected static Person GetPerson(string id)
        {
            var exist = IdExist(id);
            
            using var conn = new MySqlConnection(ConnStr);
            var reader = conn.ExecuteReader("SELECT * FROM persons_tbl WHERE PersonId = @a", new { @a = id });
            var person = new Person();
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            while (reader.Read())
            {
                person.PersonId = id;
                person.Fname = reader.GetValue(1).ToString();
                person.Lname = reader.GetValue(2).ToString();
                person.Gender = reader.GetValue(3).ToString();
                person.PrivateNumber = reader.GetValue(4).ToString();
                person.Date = Convert.ToDateTime(reader.GetValue(5).ToString()).ToString(CultureInfo.InvariantCulture);
                person.PhoneNumber = reader.GetValue(6).ToString();
                person.City = reader.GetValue(7).ToString();
                person.ImageLocation = reader.GetValue(8).ToString();
            }
            return (exist) ? person : null;
        }
    }
}