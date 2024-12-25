using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace backend.Pages.Users
{
    public class index : PageModel
    {
        private readonly ILogger<index> _logger;

        public index(ILogger<index> logger)
        {
            _logger = logger;
        }

        public List<UserInfo> Users { get; set; } = new List<UserInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Server=localhost;Database=mydatabase;User=root;Password=123456;";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Users";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserInfo user = new UserInfo();
                                user.uid = reader.GetInt32("uid");
                                user.uname = reader.GetString("uname");
                                user.upass = reader.GetString("upass");

                                Users.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("We have an error: " + ex.Message);
            }
        }

        public class UserInfo
        {
            public int uid { get; set; } = 0;
            public string uname { get; set; } = "";
            public string upass { get; set; } = "";
        }
    }
}
