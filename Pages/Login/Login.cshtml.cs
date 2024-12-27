using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;

namespace SimpleCRUDApp.Pages.Login
{
    public class Login : PageModel
    {
        [BindProperty, Required(ErrorMessage = "Please enter a user name")]
        public string uname { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "Please enter a user password")]
        public string upass { get; set; } = "";

        public string ErrorMessage { get; set; } = "";

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            try
            {
                string connectionString = "Server=localhost;Database=mydatabase;User=root;Password=123456;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Users WHERE uname = @uname";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@uname", uname);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHash = reader.GetString("upass");

                                if (BCrypt.Net.BCrypt.Verify(upass, storedHash))
                                {
                                    HttpContext.Session.SetString("Username", uname);

                                    Response.Redirect("/Users");
                                }
                                else
                                {
                                    ErrorMessage = "Invalid password.";
                                }
                            }
                            else
                            {
                                ErrorMessage = "Username not found.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
