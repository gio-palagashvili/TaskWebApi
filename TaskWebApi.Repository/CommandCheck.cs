using System;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using TaskWebApi;
// ReSharper disable LoopVariableIsNeverChangedInsideLoop
// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute

namespace TaskWeb.Repository
{
    public class CommandCheck
    {
        public string Fname(string value)
        {
            // const string gerogian = "აბგდევზთიკლმნოპჟრსტუფქღყშჩცძწჭხჯჰ";
            const string english = "abcdefghijklmnopqrstuvxyz";
            const string englishUpper = "ABCDEFGHIJKLMNOPQRSTUVXYZ";
            
            var hasEnglishLetters = value.ToCharArray().Any(x => english.Contains(x));
            var hasEnglishUpper = value.ToCharArray().Any(x => englishUpper.Contains(x));
            
            // var hasGeorgianLetters = value.ToCharArray().Any(x => gerogian.Contains(x));
            
            while (value.Length < 2 || value.Length > 50)
            {
                Console.WriteLine("Length error try again");
                value = Console.ReadLine();
            }
            while (Regex.IsMatch(value,"\\d"))
            {
                Console.WriteLine("can not contain numbers");
                value = Console.ReadLine();
            }
            
            return value;
        }
        public string Lname(string value)
        {
            const string english = "abcdefghijklmnopqrstuvxyz";
            const string englishUpper = "ABCDEFGHIJKLMNOPQRSTUVXYZ";
            
            var hasEnglishLetters = value.ToCharArray().Any(x => english.Contains(x));
            var hasEnglishUpper = value.ToCharArray().Any(x => englishUpper.Contains(x));
            
            // var hasGeorgianLetters = value.ToCharArray().Any(x => gerogian.Contains(x));
            
            while (value.Length < 2 || value.Length > 50)
            {
                Console.WriteLine("Length error try again");
                value = Console.ReadLine();
            }
            while (Regex.IsMatch(value,"\\d"))
            {
                Console.WriteLine("can not contain numbers");
                value = Console.ReadLine();
            }
            
            return value;
        }
        public string City(string value)
        {
            var hasNumbers = Regex.IsMatch(value,"\\d");
            while (hasNumbers)
            {
                Console.WriteLine("can not contain numbers");
                value = Console.ReadLine();
            }
            return value;
        }
        public string Gender(string value)
        {
            while (value != "0" && value != "1")
            {
                Console.WriteLine("0 or 1");
                value = Console.ReadLine();
            }
            return value;
        }
        public string PrivateNumber(string value)
        {
            while (value.Length != 11)
            {
                Console.WriteLine("must be 11 characters long");
                value = Console.ReadLine();
            }
            
            return value;
        }
        public string PhoneNumber(string value)
        {
            //todo home number
            
            while (value[0] != '5' && value.Length != 9)
            {
                Console.WriteLine("invalid number");
                value = Console.ReadLine();
            }
            return value;
        }
        public string Date(DateTime value)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (value.Year * 100 + value.Month) * 100 + value.Day;
            var z = ((a - b) / 10000) >= 18;
            
            Console.WriteLine("underage");
            if(z!) System.Environment.Exit(1);
            
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public string FileLocation(string value)
        {
            
            return value;
        }
    }
}