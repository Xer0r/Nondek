using CsvHelper;
using System.Globalization;
using System.IO;

namespace Registering
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ProfileImagePath { get; set; }
        public string FavoriteQuote { get; set; }
    }


    public static class CsvHelper
    {
        private static string destinationPath = Path.Combine("Users", "users.csv") /*Path.Combine(Directory.GetCurrentDirectory(), "Users", "users.csv")*/;

    public static List<User> ReadUsers()
        {
            if (!File.Exists(destinationPath))
                return new List<User>();

            using (var reader = new StreamReader(destinationPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<User>().ToList();
            }
        }

        public static void WriteUsers(List<User> users)
        {
            using (var writer = new StreamWriter(destinationPath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(users);
            }
        }
    }
}
