using System;
using System.Text.RegularExpressions;
using TaskWebApi;
using TaskWebApi.Repository.Dapper;

namespace TaskWeb.Repository
{
    public class PersonVerify : Connection
    {
        public enum ErrorList
        {
            Ok = 0,
            NamesDupe = 1,
            PrivateNumberDupe = 2,
            PhoneNumberDupe = 3,
            NameContainsNumbers = 4,
            CityContainsNumbers = 5,
            BinaryGender = 6,
            InvalidPhone = 7,
            UnderAged = 8,
            FirstNameLength = 9,
            LastNameLength = 10,
            PhoneNumberLength = 11,
            PrivateNumberLength = 12,
            InvalidFormatDate = 13,
            PrivateNumberLetters = 14,
        }
        private static Enum Fname(string value)
        {
            // const string gerogian = "აბგდევზთიკლმნოპჟრსტუფქღყშჩცძწჭხჯჰ";
            // const string english = "abcdefghijklmnopqrstuvxyz";
            // const string englishUpper = "ABCDEFGHIJKLMNOPQRSTUVXYZ";

            // var hasEnglishLetters = value.ToCharArray().Any(x => english.Contains(x));
            // var hasEnglishUpper = value.ToCharArray().Any(x => englishUpper.Contains(x));

            // var hasGeorgianLetters = value.ToCharArray().Any(x => gerogian.Contains(x));
            if (value.Length < 2 || value.Length > 50)
            {
                return ErrorList.FirstNameLength;
            }
            if (Regex.IsMatch(value, "\\d"))
            {
                return ErrorList.NameContainsNumbers;
            }
            return FnameExists(value).Result ? ErrorList.NamesDupe : ErrorList.Ok;
        }
        private static Enum Lname(string value)
        {
            // const string english = "abcdefghijklmnopqrstuvxyz";
            // const string englishUpper = "ABCDEFGHIJKLMNOPQRSTUVXYZ";

            // var hasEnglishLetters = value.ToCharArray().Any(x => english.Contains(x));
            // var hasEnglishUpper = value.ToCharArray().Any(x => englishUpper.Contains(x));

            // var hasGeorgianLetters = value.ToCharArray().Any(x => gerogian.Contains(x));
            if (value.Length < 2 || value.Length > 50)
            {
                return ErrorList.LastNameLength;
            }
            if (Regex.IsMatch(value, "\\d"))
            {
                return ErrorList.NameContainsNumbers;
            }
            return LnameExists(value).Result ? ErrorList.NamesDupe : ErrorList.Ok;

        }
        private static Enum City(string value)
        {
            var hasNumbers = Regex.IsMatch(value, "\\d");
            return (hasNumbers) ? ErrorList.CityContainsNumbers : ErrorList.Ok;
        }
        private static Enum Gender(string value)
        {
            return (value == "0" || value == "1") ? ErrorList.Ok : ErrorList.BinaryGender;
        }
        private static Enum PrivateNumber(string value)
        {
            if (!Regex.IsMatch(value, @"[A-Z][a-z]"))
            {
                return ErrorList.PrivateNumberLetters;
            }
            
            return (value.Length == 11) ? ErrorList.Ok : ErrorList.PrivateNumberLength;
        }
        private static Enum PhoneNumber(string value)
        {
            //todo home number
            return (value[0] == '5' || value.Length == 9) ? ErrorList.Ok : ErrorList.InvalidPhone;
        }
        private static Enum Date(DateTime value)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (value.Year * 100 + value.Month) * 100 + value.Day;
            var z = ((a - b) / 10000);

            return (z >= 18) ? ErrorList.Ok : ErrorList.UnderAged;
        }
        // private static bool VerifyFile(string path, string extension)
        // {
        //     var l = new List<string>() {".jpg", ".png", ".jpeg"};
        //
        //     return l.Contains(extension);
        // }
        // public static string FileLocation(string value, string name)
        // {
        //     var path = $"{value}";
        //     var path2 = @$"C:\Users\Gio\Documents\c#\TaskWebApi\images\{name}.png";
        //
        //     while (!File.Exists(path))
        //     {
        //         Console.WriteLine("file does not exist");
        //         path = Console.ReadLine();
        //     }
        //
        //     var extension = Path.GetExtension(path);
        //
        //     while (!VerifyFile(path, extension))
        //     {
        //         Console.WriteLine(
        //             "invalid file extension only jpg, jpeg and png are allowed(change the extension and retype the path below)");
        //         path = Console.ReadLine();
        //         extension = Path.GetExtension(path);
        //     }
        //
        //     File.Copy(path, path2);
        //
        //     return Path.GetRelativePath(Directory.GetCurrentDirectory(), path2);
        // }
        public static Enum Verify(Person person)
        {
            var validFname = Fname(person.Fname);
            if (Convert.ToInt32(validFname) == 1) return ErrorList.NamesDupe;
            if (Convert.ToInt32(validFname) == 2) return ErrorList.FirstNameLength;
            
            var validLname = Lname(person.Lname);
            if (Convert.ToInt32(validLname) == 4) return ErrorList.NamesDupe;
            if (Convert.ToInt32(validLname) == 2) return ErrorList.LastNameLength;
            
            var validCity = City(person.City);
            if (Convert.ToInt32(validCity) == 1) return ErrorList.CityContainsNumbers;
            
            var validGender= Gender(person.Gender);
            if (Convert.ToInt32(validGender) == 5) return ErrorList.BinaryGender;
            
            //same private numbers arent allowed
            var validPrivate = PrivateNumber(person.PrivateNumber);
            if (Convert.ToInt32(validPrivate) == 12) return ErrorList.PrivateNumberLength;
            if (Convert.ToInt32(validPrivate) == 2) return ErrorList.PrivateNumberDupe;

            //todo office numbers
            var validPhone = PhoneNumber(person.PhoneNumber);
            if (Convert.ToInt32(validPhone) == 7) return ErrorList.InvalidPhone;
            if (Convert.ToInt32(validPhone) == 11) return ErrorList.PhoneNumberLength;

            var validDate =  Date(Convert.ToDateTime(person.Date));
            if (Convert.ToInt32(validDate) == 8) return ErrorList.UnderAged;
            return Convert.ToInt32(validDate) == 13 ? ErrorList.InvalidFormatDate : ErrorList.Ok;
        }
    }

    internal class PersonVerifyImpl : PersonVerify
    {
    }
}