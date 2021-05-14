using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TaskWebApi;
using TaskWebApi.Repository.Dapper;

// ReSharper disable LoopVariableIsNeverChangedInsideLoop
// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace TaskWeb.Repository
{
    public class CommandCheck : Connection
    {
        public static string Fname(string value)
        {
            // const string gerogian = "აბგდევზთიკლმნოპჟრსტუფქღყშჩცძწჭხჯჰ";
            // const string english = "abcdefghijklmnopqrstuvxyz";
            // const string englishUpper = "ABCDEFGHIJKLMNOPQRSTUVXYZ";

            // var hasEnglishLetters = value.ToCharArray().Any(x => english.Contains(x));
            // var hasEnglishUpper = value.ToCharArray().Any(x => englishUpper.Contains(x));

            // var hasGeorgianLetters = value.ToCharArray().Any(x => gerogian.Contains(x));

            while (value.Length < 2 || value.Length > 50)
            {
                Console.WriteLine("Length error try again");
                value = Console.ReadLine();
            }
            while (Regex.IsMatch(value, "\\d"))
            {
                Console.WriteLine("can not contain numbers");
                value = Console.ReadLine();
            }

            return value;
        }
        public static string Lname(string value)
        {
            // const string english = "abcdefghijklmnopqrstuvxyz";
            // const string englishUpper = "ABCDEFGHIJKLMNOPQRSTUVXYZ";

            // var hasEnglishLetters = value.ToCharArray().Any(x => english.Contains(x));
            // var hasEnglishUpper = value.ToCharArray().Any(x => englishUpper.Contains(x));

            // var hasGeorgianLetters = value.ToCharArray().Any(x => gerogian.Contains(x));

            while (value.Length < 2 || value.Length > 50)
            {
                Console.WriteLine("Length error try again");
                value = Console.ReadLine();
            }
            while (Regex.IsMatch(value, "\\d"))
            {
                Console.WriteLine("can not contain numbers");
                value = Console.ReadLine();
            }

            return value;
        }
        public static string City(string value)
        {
            var hasNumbers = Regex.IsMatch(value, "\\d");
            while (hasNumbers)
            {
                Console.WriteLine("can not contain numbers");
                value = Console.ReadLine();
            }
            return value;
        }
        public static string Gender(string value)
        {
            while (value != "0" && value != "1")
            {
                Console.WriteLine("0 or 1");
                value = Console.ReadLine();
            }
            return value;
        }
        public static string PrivateNumber(string value)
        {
            while (value.Length != 11)
            {
                Console.WriteLine("must be 11 characters long");
                value = Console.ReadLine();
            }

            return value;
        }
        public static string PhoneNumber(string value)
        {
            //todo home number

            while (value[0] != '5' && value.Length != 9)
            {
                Console.WriteLine("invalid number");
                value = Console.ReadLine();
            }
            return value;
        }
        public static string Date(DateTime value)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (value.Year * 100 + value.Month) * 100 + value.Day;
            var z = ((a - b) / 10000);

            return z >= 18 ? z.ToString() : "underage";
        }
        private static bool VerifyFile(string path,string extension)
        {
            var l = new List<string>(){".jpg",".png",".jpeg"};
            
            return l.Contains(extension);
        }
        public static string FileLocation(string value, string name)
        {
            var path = $"{value}";
            var path2 = @$"C:\Users\Gio\Documents\c#\TaskWebApi\images\{name}.png";

            while (!File.Exists(path))
            {
                Console.WriteLine("file does not exist");
                path = Console.ReadLine();
            }
            var extension = Path.GetExtension(path);
            
            while (!VerifyFile(path,extension))
            {
                Console.WriteLine("invalid file extension only jpg, jpeg and png are allowed(change the extension and retype the path below)");
                path = Console.ReadLine();
                extension = Path.GetExtension(path);
            }
            File.Copy(path, path2);
            
            return Path.GetRelativePath(Directory.GetCurrentDirectory(),path2);
        }
        private static string GenerateRandomId()
        {
            var random = new Random();
            return random.Next(10000,1000000000).ToString();
        }
        private string RandomId { get; set; }
    }
}