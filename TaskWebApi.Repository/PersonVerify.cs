using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using TaskWebApi;
using TaskWebApi.Repository.Dapper;
// ReSharper disable ClassNeverInstantiated.Global

namespace TaskWeb.Repository
{
    public class PersonVerify : Connection
    {
        private static ErrorClass Fname(string value)
        {
            // const string gerogian = "აბგდევზთიკლმნოპჟრსტუფქღყშჩცძწჭხჯჰ";
            // const string english = "abcdefghijklmnopqrstuvxyz";
            // const string englishUpper = "ABCDEFGHIJKLMNOPQRSTUVXYZ";

            // var hasEnglishLetters = value.ToCharArray().Any(x => english.Contains(x));
            // var hasEnglishUpper = value.ToCharArray().Any(x => englishUpper.Contains(x));

            // var hasGeorgianLetters = value.ToCharArray().Any(x => gerogian.Contains(x));
            
            if (value.Length is < 2 or > 50)
            {
                return new ErrorClass() { ErrorCode = ErrorList.ERROR_DUPLICATE, Description = "Length Error" };
            }
            return Regex.IsMatch(value, "\\d") ? new ErrorClass() { ErrorCode = ErrorList.ERROR_INVALID_INPUT, Description = "name contains numbers" } : new ErrorClass { ErrorCode = ErrorList.OK };
        }
        private static ErrorClass Lname(string value, string value2)
        {
            // const string english = "abcdefghijklmnopqrstuvxyz";
            // const string englishUpper = "ABCDEFGHIJKLMNOPQRSTUVXYZ";

            // var hasEnglishLetters = value.ToCharArray().Any(x => english.Contains(x));
            // var hasEnglishUpper = value.ToCharArray().Any(x => englishUpper.Contains(x));

            // var hasGeorgianLetters = value.ToCharArray().Any(x => gerogian.Contains(x));
            //todo

            if (value.Length is < 2 or > 50)
            {
                return new ErrorClass { ErrorCode = ErrorList.ERROR_INVALID_INPUT, Description = "Last name length error" };
            }
            if (Regex.IsMatch(value, "\\d"))
            {
                return new ErrorClass { ErrorCode = ErrorList.ERROR_INVALID_INPUT, Description = "name contains numbers" };
            }
            return NameExists(value, value2).Result ? new ErrorClass { ErrorCode = ErrorList.ERROR_DUPLICATE, Description = "name already exists" } : new ErrorClass { ErrorCode = ErrorList.OK };

        }
        private static ErrorClass City(string value)
        {
            var hasNumbers = Regex.IsMatch(value, "\\d");
            return (hasNumbers) ? new ErrorClass { ErrorCode = ErrorList.ERROR_INVALID_INPUT, Description = "City Contains Numbers" } : new ErrorClass { ErrorCode = ErrorList.OK };
        }
        private static ErrorClass Gender(string value)
        {
            return (value is "0" or "1") ? new ErrorClass { ErrorCode = ErrorList.OK } : new ErrorClass { ErrorCode = ErrorList.ERROR_INVALID_INPUT, Description = "Gender must be either 0 or 1" };
        }
        public static ErrorClass PrivateNumber(string value)
        {
            if (!Regex.IsMatch(value, @"[A-Za-z]"))
            {
                return new ErrorClass { ErrorCode = ErrorList.ERROR_INVALID_INPUT, Description = "" };
            }

            return (value.Length == 11) 
                ? new ErrorClass { ErrorCode = ErrorList.OK } 
                : new ErrorClass { ErrorCode = ErrorList.ERROR_INVALID_INPUT, Description = "private numbers must be 11 digits" };
        }
        private static ErrorClass PhoneNumber(string value)
        {
            //todo home number
            return (value[0] == '5' || value.Length == 9) ? new ErrorClass { ErrorCode = ErrorList.OK } : new ErrorClass { ErrorCode = ErrorList.ERROR_INVALID_INPUT, Description = "invalid phone number" };
        }

        public static ErrorClass Date(DateTime value)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (value.Year * 100 + value.Month) * 100 + value.Day;
            var z = ((a - b) / 10000);

            return (z >= 18) ? new ErrorClass { ErrorCode = ErrorList.OK } : new ErrorClass { ErrorCode = ErrorList.ERROR_INVALID_INPUT, Description = "under aged" };
        }
        private static bool VerifyFile(string path)
        {
            var l = new List<string>() {".jpg", ".png", ".jpeg"};
            var extension = Path.GetExtension(path);
            return l.Contains(extension);
        }
        private static ErrorClass GetFile(string value)
        {
            var path = $"{value}";
            return VerifyFile(path)
                ? new ErrorClass {ErrorCode = ErrorList.OK}
                : new ErrorClass {ErrorCode = ErrorList.INCORRECT_FORMAT_FILE};
        }
        
        public static ErrorClass Verify(Person person)
        {
            var validFname = Fname(person.Fname);
            if (validFname.ErrorCode != ErrorList.OK) return validFname;

            var validLname = Lname(person.Lname, person.Fname);
            if (validLname.ErrorCode != ErrorList.OK) return validLname;

            var validCity = City(person.City);
            if (validCity.ErrorCode != ErrorList.OK) return validCity;

            var validGender = Gender(person.Gender);
            if (validGender.ErrorCode != ErrorList.OK) return validGender;

            var validPrivate = PrivateNumber(person.PrivateNumber);
            if (validPrivate.ErrorCode != ErrorList.OK) return validPrivate;

            var validPhone = PhoneNumber(person.PhoneNumber);
            if (validPhone.ErrorCode != ErrorList.OK) return validPhone;

            var validDate = Date(Convert.ToDateTime(person.Date));
            if (validDate.ErrorCode != ErrorList.OK) return validDate;

            var validFile = GetFile(person.ImageLocation);
            if (validFile.ErrorCode != ErrorList.OK) return validFile;

            return new ErrorClass { ErrorCode = ErrorList.OK, Description = "user inserted" };
        }
    }
}