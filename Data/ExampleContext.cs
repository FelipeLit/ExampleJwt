using Backend.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Data;

    public class ExampleContext : DbContext
    {
        public ExampleContext(DbContextOptions<ExampleContext> options) : base (options)
        {

        }
        
        public DbSet <Usuarios> Usuarios { get; set;}
        public DbSet <Product> Product { get; set;}
}
