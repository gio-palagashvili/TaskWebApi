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
        public InsertPersonRep(Person person)
        {
            Insert(new Person()
            {
                PersonId = person.PersonId,
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
    }
}