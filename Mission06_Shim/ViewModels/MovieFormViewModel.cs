using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mission06_Shim.ViewModels;

public class MovieFormViewModel
{
    [Required]
    [Display(Name = "Category")]
    public string CategoryName { get; set; } = string.Empty;

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Range(1888, 2100)]
    public int? Year { get; set; }

    [Required]
    public string Director { get; set; } = string.Empty;

    [Required]
    public string Rating { get; set; } = string.Empty;

    public bool? Edited { get; set; }

    [Display(Name = "Lent To")]
    public string? LentTo { get; set; }

    [StringLength(25)]
    public string? Notes { get; set; }

    public IEnumerable<SelectListItem> Ratings { get; set; } = Array.Empty<SelectListItem>();
}

