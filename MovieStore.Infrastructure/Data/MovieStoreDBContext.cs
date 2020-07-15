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

        public DbSet<Trailer> Trailers { get; set; } 

        public DbSet<MovieGenre> MovieGenres { get; set; }

        public DbSet<Cast> Casts { get; set; }

        public DbSet<MovieCast> MovieCasts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Favorite> Favorites { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<Role>(ConfigureRole);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> modelBuilder) {

            //we can use Fluent API Configurations to model our tables

            modelBuilder.ToTable("Movie");
            modelBuilder.HasKey(m=>m.Id);
            modelBuilder.Property(m => m.Title).IsRequired().HasMaxLength(256);
            modelBuilder.Property(m => m.Overview).HasMaxLength(4096);
            modelBuilder.Property(m => m.Tagline).HasMaxLength(512);
            modelBuilder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.PosterUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            modelBuilder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            modelBuilder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
            // we don't want to Create Rating Column but we want C# rating property in our Entity so that we can show Movie rating in the UI
            modelBuilder.Ignore(m => m.Rating);

        }


        public void ConfigureTrailer(EntityTypeBuilder<Trailer> modelBuilder) {

            modelBuilder.ToTable("Trailer");
            modelBuilder.HasKey(t => t.Id);
            modelBuilder.Property(t => t.Name).HasMaxLength(2084);
            modelBuilder.Property(t => t.TrailerUrl).HasMaxLength(2084);
        }

        public void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> modelBuilder) {

            modelBuilder.ToTable("MovieGenre");
            modelBuilder.HasKey(mg =>new {mg.MovieId, mg.GenreId });
            modelBuilder.HasOne(mg=> mg.Movie).WithMany(g=>g.MovieGenres).HasForeignKey(mg=>mg.MovieId);
            modelBuilder.HasOne(mg => mg.Genre).WithMany(g => g.MovieGenres).HasForeignKey(mg=>mg.GenreId);


        }

        public void ConfigureCast(EntityTypeBuilder<Cast> modelBuilder) {

            modelBuilder.ToTable("Cast");
            modelBuilder.HasKey(c=> c.Id);
            modelBuilder.Property(c=> c.Name).HasMaxLength(128);
            modelBuilder.Property(c=> c.Gender).HasMaxLength(4096);
            modelBuilder.Property(c => c.TmdbUrl).HasMaxLength(4096);
            modelBuilder.Property(c=> c.ProfilePath).HasMaxLength(2084);
        }

        public void ConfigureMovieCast(EntityTypeBuilder<MovieCast> modelBuilder) {
            modelBuilder.ToTable("MovieCast");
            modelBuilder.HasKey(mc=> new { mc.CastId, mc.MovieId, mc.Character});
            modelBuilder.HasOne(mc=> mc.Cast).WithMany(c=> c.MovieCasts).HasForeignKey(mc=> mc.CastId);
            modelBuilder.HasOne(mc=> mc.Movie).WithMany(m=> m.MovieCasts).HasForeignKey(mc=> mc.MovieId);
            modelBuilder.Property(mc=> mc.Character).HasMaxLength(450);
        
        }

        public void ConfigureUser(EntityTypeBuilder<User> modelBuilder) {
            modelBuilder.ToTable("User");
            modelBuilder.HasKey(u=> u.Id);
            modelBuilder.Property(u=> u.FirstName).HasMaxLength(128);
            modelBuilder.Property(u=> u.LastName).HasMaxLength(128);
            modelBuilder.Property(u=> u.Email).HasMaxLength(256);
            modelBuilder.Property(u=> u.HashedPassword).HasMaxLength(4096);
            modelBuilder.Property(u=> u.PhoneNumber).HasMaxLength(4096);
            modelBuilder.Property(u => u.Salt).HasMaxLength(4096);


        }


        public void ConfigureReview(EntityTypeBuilder<Review> modelBuilder) {

            modelBuilder.ToTable("Review");
            modelBuilder.HasKey(r=> new { r.MovieId, r.UserId});
            modelBuilder.HasOne(r=> r.User).WithMany(r=> r.Reviews);
            modelBuilder.HasOne(r=> r.Movie).WithMany(r=> r.Reviews);
            modelBuilder.Property(r=> r.Rating).HasColumnType("decimal(3, 2)");
            modelBuilder.Property(r=> r.ReviewText).HasMaxLength(4096);

        }

        public void ConfigurePurchase(EntityTypeBuilder<Purchase> modelBuilder) {

            modelBuilder.ToTable("Purchase");
            modelBuilder.HasKey(p=> p.Id);
            modelBuilder.HasIndex(p => p.PurchaseNumber).IsUnique();
            modelBuilder.Property(p=> p.TotalPrice).HasColumnType("decimal(5,2)");
            modelBuilder.HasOne(p=> p.Customer).WithMany(p=> p.Purchases);
            
        }

        public void ConfigureRole(EntityTypeBuilder<Role> modelBuilder) {
            modelBuilder.ToTable("Role");
            modelBuilder.HasKey(r=> r.Id);
            modelBuilder.Property(r=> r.Name).HasMaxLength(20);
        
        }

        public void ConfigureUserRole(EntityTypeBuilder<UserRole> modelBuilder)
        {

            modelBuilder.ToTable("UserRole");
            modelBuilder.HasKey(ur=> new { ur.UserId, ur.RoleId});
            modelBuilder.HasOne(u => u.User).WithMany(ur=> ur.UserRoles).HasForeignKey(ur=> ur.UserId);
            modelBuilder.HasOne(r=> r.Role).WithMany(ur=> ur.UserRoles).HasForeignKey(ur=> ur.RoleId);

        }

        public void ConfigureFavorite(EntityTypeBuilder<Favorite> modelBuilder) {
            modelBuilder.ToTable("Favorite");
            modelBuilder.HasKey(f=> f.Id);
            modelBuilder.HasOne(r => r.User).WithMany(r => r.Favorites);



        }
             

    }
}
