using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace SimpleCRUDApp.Pages.Users
{
    public class Update : PageModel
    {
        [BindProperty, Required(ErrorMessage = "Please enter a user ID")]
        public int uid { get; set; }
        [BindProperty, Required(ErrorMessage = "Please enter a user name")]
        public string uname { get; set; } = "";
        [BindProperty, Required(ErrorMessage = "Please enter a user password")]
        public string upass { get; set; } = "";

        public string ErrorMessage { get; set; } = "";

        public void OnGet(int uid)
        {
            try
            {
                string connectionString = "Server=localhost;Database=mydatabase;User=root;Password=root;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Users WHERE uid = @uid";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@uid", uid);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                this.uid = reader.GetInt32(0);
                                uname = reader.GetString(1);
                                upass = reader.GetString(2);
                            }
                            else
                            {
                                Response.Redirect("/Users/Index");
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

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            try
            {
                string hashedPassword = upass;
                if (!string.IsNullOrEmpty(upass))
                {
                    hashedPassword = BCrypt.Net.BCrypt.HashPassword(upass);
                }

                string connectionString = "Server=localhost;Database=mydatabase;User=root;Password=root;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "UPDATE Users SET uname = @uname, upass = @upass WHERE uid = @uid";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@uid", uid);
                        command.Parameters.AddWithValue("@uname", uname);
                        command.Parameters.AddWithValue("@upass", hashedPassword);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            ErrorMessage = "Update failed. No user found with the given ID.";
                        }
                        else
                        {
                            Response.Redirect("/Users/Index");
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