using System.ComponentModel.DataAnnotations;

namespace Mission06_Shim.Models;

public class Category
{
    public int CategoryId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
}

