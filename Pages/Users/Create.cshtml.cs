using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using BCrypt.Net;

namespace SimpleCRUDApp.Pages.Users
{
    public class Create : PageModel
    {
        [BindProperty, Required(ErrorMessage = "Please enter a user name")]
        public string uname { get; set; } = "";
        [BindProperty, Required(ErrorMessage = "Please enter a user password")]
        public string upass { get; set; } = "";

        public void OnGet() { }

        public string ErrorMessage { get; set; } = "";

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(upass);

                string connectionString = "Server=localhost;Database=mydatabase;User=root;Password=123456;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Users (uname, upass) VALUES (@uname, @upass)";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@uname", uname);
                        command.Parameters.AddWithValue("@upass", hashedPassword);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Users/Index");
        }
    }
}
