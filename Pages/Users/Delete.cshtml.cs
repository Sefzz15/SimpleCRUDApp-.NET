using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace SimpleCRUDApp.Pages.Users
{
    public class Delete : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost(int uid)
        {
            deleteUser(uid);
            Response.Redirect("/Users/Index");
        }

        private void deleteUser(int uid)
        {
            try
            {
                string connectionString = "Server=localhost;Database=mydatabase;User=root;Password=root;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "DELETE FROM Users WHERE uid = @uid";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@uid", uid);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot delete User:" + ex.Message);
            }
            Response.Redirect("/Users/Index");
        }
    }
}
