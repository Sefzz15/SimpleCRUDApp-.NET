using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBackendApp.Models;


namespace MyBackendApp.Models
{
    // Database Context
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Define a table in the database
        public required DbSet<MyTable> MyTable { get; set; }
    }

    // Model for the table
    public class MyTable
    {
        public required int uid { get; set; }
        public required string uname { get; set; }
        public required string upass { get; set; }
    }
}