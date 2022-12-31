using System.ComponentModel.DataAnnotations;

namespace JamiesRecipes.Models;

public class Recipe{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Serves { get; set; }
    public string? Ingredients { get; set; }
    public string? Method { get; set; }
    public string? Dietary { get; set; }
}