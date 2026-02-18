using Microsoft.EntityFrameworkCore;

namespace Mission06_Shim.Models;

public class MovieContext : DbContext
{
    public MovieContext(DbContextOptions<MovieContext> options) : base(options) 
    { 
    }

    public DbSet<Movie> Movies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>().HasData(
            new Movie
            {
                MovieId = 1,
                Category = "Sci-Fi",
                Title = "Inception",
                Year = 2010,
                Director = "Christopher Nolan",
                Rating = "PG-13",
                Edited = false,
                LentTo = "",
                Notes = "Dream within a dream"
            },
            new Movie
            {
                MovieId = 2,
                Category = "Action",
                Title = "The Dark Knight",
                Year = 2008,
                Director = "Christopher Nolan",
                Rating = "PG-13",
                Edited = false,
                LentTo = "",
                Notes = "Why so serious?"
            },
            new Movie
            {
                MovieId = 3,
                Category = "Drama",
                Title = "Parasite",
                Year = 2019,
                Director = "Bong Joon-ho",
                Rating = "R",
                Edited = false,
                LentTo = "",
                Notes = "Masterpiece"
            }
        );
    }
}