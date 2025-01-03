using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace SimpleCRUDApp.Pages.Users
{
    public class indexModel : PageModel
    {
        private readonly ILogger<indexModel> _logger;

        public indexModel(ILogger<indexModel> logger)
        {
            _logger = logger;
        }

        public List<UserInfo> Users { get; set; } = new List<UserInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Server=localhost;Database=mydatabase;User=root;Password=root;";
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
