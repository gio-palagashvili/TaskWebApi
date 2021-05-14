// ReSharper disable all UnusedMember.Local

using System;
using System.Collections.Generic;

namespace TaskWebApi
{
    public class Person
    {
        private string Index { get; set; }
        public string PersonId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Gender { get; set; }
        public string PrivateNumber { get; set; }
        public string Date { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageLocation { get; set; }
        public List<string> Relations { get; set; }
    }
}