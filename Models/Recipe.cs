using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JamiesRecipes.Models;

public class Recipe{
    public int Id { get; set; }

    [Required]
    public string? Title { get; set; }

    [Required]
    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    [Range(1, 20)]
    public int Serves { get; set; }

    [Required]
    [DataType(DataType.MultilineText)]
    [UIHint("DisplayRecipe")]
    public string? Ingredients { get; set; }

    [Required]
    [DataType(DataType.MultilineText)]
    [UIHint("DisplayRecipe")]
    public string? Method { get; set; }

    [Required]
    public string? Dietary { get; set; }

    [DisplayName("Image")]
    public string? ImageUrl { get; set; }
}