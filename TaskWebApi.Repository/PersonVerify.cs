using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TaskWebApi;

namespace TaskWeb.Repository
{
    public static class PersonVerify
    {
        private static bool Fname(string value)
        {
            // const string gerogian = "აბგდევზთიკლმნოპჟრსტუფქღყშჩცძწჭხჯჰ";
            // const string english = "abcdefghijklmnopqrstuvxyz";
            // const string englishUpper = "ABCDEFGHIJKLMNOPQRSTUVXYZ";

            // var hasEnglishLetters = value.ToCharArray().Any(x => english.Contains(x));
            // var hasEnglishUpper = value.ToCharArray().Any(x => englishUpper.Contains(x));

            // var hasGeorgianLetters = value.ToCharArray().Any(x => gerogian.Contains(x));
            if (value.Length < 2 || value.Length > 50)
            {
                return false;
            }
            return !Regex.IsMatch(value, "\\d");
        }

        private static bool Lname(string value)
        {
            // const string english = "abcdefghijklmnopqrstuvxyz";
            // const string englishUpper = "ABCDEFGHIJKLMNOPQRSTUVXYZ";

            // var hasEnglishLetters = value.ToCharArray().Any(x => english.Contains(x));
            // var hasEnglishUpper = value.ToCharArray().Any(x => englishUpper.Contains(x));

            // var hasGeorgianLetters = value.ToCharArray().Any(x => gerogian.Contains(x));
            if (value.Length < 2 || value.Length > 50)
            {
                return false;
            }
            return !Regex.IsMatch(value, "\\d");
        }

        private static bool City(string value)
        {
            var hasNumbers = Regex.IsMatch(value, "\\d");
            return !hasNumbers;
        }

        private static bool Gender(string value)
        {
            return value == "0" || value == "1";
        }

        private static bool PrivateNumber(string value)
        {
            return value.Length == 11;
        }

        private static bool PhoneNumber(string value)
        {
            //todo home number
            return value[0] == '5' || value.Length == 9;
        }

        private static bool Date(DateTime value)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (value.Year * 100 + value.Month) * 100 + value.Day;
            var z = ((a - b) / 10000);

            return z >= 18;
        }
        private static bool VerifyFile(string path, string extension)
        {
            var l = new List<string>() {".jpg", ".png", ".jpeg"};

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

            while (!VerifyFile(path, extension))
            {
                Console.WriteLine(
                    "invalid file extension only jpg, jpeg and png are allowed(change the extension and retype the path below)");
                path = Console.ReadLine();
                extension = Path.GetExtension(path);
            }

            File.Copy(path, path2);

            return Path.GetRelativePath(Directory.GetCurrentDirectory(), path2);
        }

        public static string Verify(Person person)
        {
            //todo same names arent allowed
            var validFname = Fname(person.Fname);
            if (!validFname) return "fname";
            
            var validLname = Fname(person.Lname);
            if (!validLname) return "lname";
            
            var validCity = City(person.City);
            if (!validCity) return "city";
            
            var validGender= Gender(person.Gender);
            if (!validGender) return "gender";
            //same private numbers arent allowed
            var validPrivate = PrivateNumber(person.PrivateNumber);
            if (!validPrivate) return "private";
            //todo office numbers
            var validPhone = PhoneNumber(person.PhoneNumber);
            if (!validPhone) return "phone";
            
            var validDate =  Date(Convert.ToDateTime(person.Date));
            if (!validDate) return "date";
            
            return "200";
        }
    }
}