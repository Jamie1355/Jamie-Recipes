using System.ComponentModel.DataAnnotations;

namespace JamiesRecipes.Models;

public class Recipe{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Serves { get; set; }

    [DataType(DataType.MultilineText)]
    [UIHint("DisplayRecipe")]
    public string? Ingredients { get; set; }

    [DataType(DataType.MultilineText)]
    [UIHint("DisplayRecipe")]
    public string? Method { get; set; }
    public string? Dietary { get; set; }
}