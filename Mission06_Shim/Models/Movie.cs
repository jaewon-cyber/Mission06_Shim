using System.ComponentModel.DataAnnotations;

namespace Mission06_Shim.Models;

public class Movie
{
    [Key]
    [Required]
    public int MovieId { get; set; }

    [Required(ErrorMessage = "Category is required.")]
    public string Category { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Year is required.")]
    [Range(1888, int.MaxValue, ErrorMessage = "Year must be after 1888.")] // 1888년 제한
    public int Year { get; set; }

    [Required(ErrorMessage = "Director is required.")]
    public string Director { get; set; }

    [Required(ErrorMessage = "Rating is required.")]
    public string Rating { get; set; }

    public bool Edited { get; set; }

    public string? LentTo { get; set; }

    [MaxLength(25, ErrorMessage = "Notes cannot exceed 25 characters.")]
    public string? Notes { get; set; }

    
    public bool CopiedToPlex { get; set; } 
}