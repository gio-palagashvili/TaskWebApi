using System;
using System.Net;
using System.Net.Http;
using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient.Memcached;

namespace DatabaseSeeding
{
    public class CreateRandomUser
    {
        const string connStr = "server=localhost;user=root;database=taskweb_db;port=3306;password=''";
        public CreateRandomUser()
        {

            // for (var i = 0; i < 10; i++)
            // {
            const string url = "https://randomuser.me/api/";
            using var client = new HttpClient();
            // client.DownloadFileAsync(new Uri(url), @"c:\temp\image35.png");

            using var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;

            // }
        }
    }
}