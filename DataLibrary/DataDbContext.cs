using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyDataModels;

namespace DataLibrary
{
    public class DataDbContext : DbContext
    {
        private static IConfigurationRoot _configuration;

        //TODO: Add your data models here
        //Example: Categories:
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }

        public DataDbContext() : base()
        { 
            //intentionally blank
        }

        public DataDbContext(DbContextOptions options)
            : base(options)
        { 
            //intentionally empty
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO 1: If you want to change the database name, find the appsettings.json file
            //      then modify the connection string for MyDataManagerData to set a different
            //      Database name
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                _configuration = builder.Build();
                var cnstr = _configuration.GetConnectionString("MyDataManagerData");
                optionsBuilder.UseSqlServer(cnstr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //use the fluent api here to define data structures and relationships
            //
            //... [optional] ...

            //example: seed some data:
            modelBuilder.Entity<Category>(x =>
            {
                x.HasData(new Category() { Id = 1, Name = "Books" },
                            new Category() { Id = 2, Name = "Movies" },
                            new Category() { Id = 3, Name = "Games" }
                );
            });

            modelBuilder.Entity<Item>(x =>
            {
                x.HasData(new Item() { Id = 1, CategoryId = 1, Name = "The Lord of the Rings" },
                            new Item() { Id = 2, CategoryId = 1, Name = "The Sword of Shannara" },
                            new Item() { Id = 3, CategoryId = 2, Name = "Top Gun" },
                            new Item() { Id = 4, CategoryId = 3, Name = "World of Tanks" },
                            new Item() { Id = 5, CategoryId = 2, Name = "Inception" },
                            new Item() { Id = 6, CategoryId = 2, Name = "Tenet" },
                            new Item() { Id = 7, CategoryId = 3, Name = "Wordle" },
                            new Item() { Id = 8, CategoryId = 1, Name = "Programming in C#" },
                            new Item() { Id = 9, CategoryId = 1, Name = "Pro GIT" },
                            new Item() { Id = 10, CategoryId = 2, Name = "Batman Begins" },
                            new Item() { Id = 11, CategoryId = 2, Name = "Man of Steel" },
                            new Item() { Id = 12, CategoryId = 3, Name = "Monopoly" },
                            new Item() { Id = 13, CategoryId = 3, Name = "Suburbia" }
                            );
            });

        }
    }
}