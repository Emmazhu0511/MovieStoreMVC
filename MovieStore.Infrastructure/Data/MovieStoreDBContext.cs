using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Core.Entities;


namespace MovieStore.Infrastructure.Data
{
    //1. Install all the EF Core library using Nuget package manager
    //2. Create a class that inherits from DbContext class
    //3. DbContext kind of a represents your database
    //4. Create a connection string which EF is gonna use to create/access the DB, should include server name, DB name and any Credentials 
    //5. Create an Entity class, for genre table 
    //6. Make sure to add your entity class as a DbSet property inside your DbContext class
    //7. In EF, we have thing called Migrations, we are gonna use Migrations to create our DB
    //8. We need to add Migration commands to create the tables, DB etc.
    //9. Syntax: Add-Migration Migration_Name.(Make sure your migration names are meaningful, don't use names such as XYZ, abc)
    //10. When running Migration commands, make sure the project selected is the one project which has DbContext class
    //11. Make sure you add reference for library that has DbContext to your startup project, in this case MVC 
    //12. Tell MVC project that we are using Entity Framework in startup file
    //13. Add DBContext options as constructor parameter for DBContext class
    //14. After creating Migration file and verifying it we need to use "update-database" command
    //15. Whenever you change your model/entity always make sure you add new Migration. don't change in DB directly
    //16. With EF Approach never change the database directly, always change entities using data annotations or Fluent API and add new Migration
    //17. Don't add multiple migration and one update-database, should one migration one update-database

    //In EF, we have 2 ways to create our entities and model our database using code-first approach
    //1. Data Annotations is bunch of C# attributes that we can use
    //2. Fluent API is more syntax and more powerful and usally uses lanbdas
    // We can combine both DataAnnotation and Fluent API

    public class MovieStoreDBContext: DbContext
    {
        //Multiple DBSet, all the DBset you create are gonna reside in your DbContext class

        public MovieStoreDBContext(DbContextOptions<MovieStoreDBContext> options) : base(options) { 
        
        }

        //This DbSet, is gonna represent out table based on Entity class which is Genre in this case
        public DbSet<Genre> Genres { get; set; }  //Generes is the table name we are going to create in DB

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Movie>(ConfigureMovie);
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> modelBuilder) {

            //we can use Fluent API Configurations to model our tables

            modelBuilder.ToTable("Movie");
            modelBuilder.HasKey(m=>m.Id);
            modelBuilder.Property(m => m.Title).IsRequired().HasMaxLength(256);

        }




    }
}
