using Microsoft.EntityFrameworkCore;

namespace Mission06_Shim.Models;

public class MovieContext : DbContext
{
    public MovieContext(DbContextOptions<MovieContext> options) : base(options)
    {
    }

    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

        modelBuilder.Entity<Movie>()
            .Property(m => m.Rating)
            .HasMaxLength(5);

        // Seed a few favorites so the database isn't empty for grading/demo.
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Action/Adventure" },
            new Category { CategoryId = 2, Name = "Drama" },
            new Category { CategoryId = 3, Name = "Comedy" }
        );

        modelBuilder.Entity<Movie>().HasData(
            new Movie
            {
                MovieId = 1,
                CategoryId = 1,
                Title = "The Lord of the Rings: The Fellowship of the Ring",
                Year = 2001,
                Director = "Peter Jackson",
                Rating = "PG-13",
                Edited = false,
                LentTo = null,
                Notes = "Extended"
            },
            new Movie
            {
                MovieId = 2,
                CategoryId = 2,
                Title = "Interstellar",
                Year = 2014,
                Director = "Christopher Nolan",
                Rating = "PG-13",
                Edited = null,
                LentTo = null,
                Notes = "IMAX"
            },
            new Movie
            {
                MovieId = 3,
                CategoryId = 3,
                Title = "The Princess Bride",
                Year = 1987,
                Director = "Rob Reiner",
                Rating = "PG",
                Edited = null,
                LentTo = null,
                Notes = "Classic"
            }
        );
    }
}