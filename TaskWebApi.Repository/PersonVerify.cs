using System;
using System.Text.RegularExpressions;
using TaskWebApi;
using TaskWebApi.Repository.Dapper;

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
            if (value.Length < 2 || value.Length > 50)
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

            if (value.Length < 2 || value.Length > 50)
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
            return (value == "0" || value == "1") ? new ErrorClass { ErrorCode = ErrorList.OK } : new ErrorClass { ErrorCode = ErrorList.ERROR_INVALID_INPUT, Description = "Gender must be either 0 or 1" };
        }
        private static ErrorClass PrivateNumber(string value)
        {
            if (!Regex.IsMatch(value, @"[A-Za-z]"))
            {
                return new ErrorClass { ErrorCode = ErrorList.ERROR_INVALID_INPUT, Description = "" };
            }

            return (value.Length == 11) ? new ErrorClass { ErrorCode = ErrorList.OK } : new ErrorClass { ErrorCode = ErrorList.ERROR_INVALID_INPUT, Description = "private numbers must be 11 digits" };
        }
        private static ErrorClass PhoneNumber(string value)
        {
            //todo home number
            return (value[0] == '5' || value.Length == 9) ? new ErrorClass { ErrorCode = ErrorList.OK } : new ErrorClass { ErrorCode = ErrorList.ERROR_INVALID_INPUT, Description = "invalid phone number" };
        }
        private static ErrorClass Date(DateTime value)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (value.Year * 100 + value.Month) * 100 + value.Day;
            var z = ((a - b) / 10000);

            return (z >= 18) ? new ErrorClass { ErrorCode = ErrorList.OK } : new ErrorClass { ErrorCode = ErrorList.ERROR_INVALID_INPUT, Description = "under aged" };
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
        public static ErrorClass Verify(Person person)
        {
            var validFname = Fname(person.Fname);
            if (validFname.ErrorCode != ErrorList.OK) return validFname;

            var validLname = Lname(person.Lname, person.Fname);
            if (validLname.ErrorCode != ErrorList.OK) return validLname;

            var validCity = City(person.City);
            if (validCity.ErrorCode != ErrorList.OK) return validCity;

            var validGender = Gender(person.Gender);
            if (validCity.ErrorCode != ErrorList.OK) return validCity;

            var validPrivate = PrivateNumber(person.PrivateNumber);
            if (validPrivate.ErrorCode != ErrorList.OK) return validPrivate;

            var validPhone = PhoneNumber(person.PhoneNumber);
            if (validPhone.ErrorCode != ErrorList.OK) return validPhone;

            var validDate = Date(Convert.ToDateTime(person.Date));
            if (validDate.ErrorCode != ErrorList.OK) return validDate;

            return new ErrorClass { ErrorCode = ErrorList.OK, Description = "user inserted" };
        }
    }
    internal class PersonVerifyImpl : PersonVerify
    {
    }
}