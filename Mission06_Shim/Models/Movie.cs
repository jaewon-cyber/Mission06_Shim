using System.ComponentModel.DataAnnotations;

namespace Mission06_Shim.Models;

public class Movie
{
    public int MovieId { get; set; }

    [Required]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Range(1888, 2100)]
    public int Year { get; set; }

    [Required]
    public string Director { get; set; } = string.Empty;

    [Required]
    public string Rating { get; set; } = string.Empty; // G, PG, PG-13, R

    public bool? Edited { get; set; }

    public string? LentTo { get; set; }

    [StringLength(25)]
    public string? Notes { get; set; }
}