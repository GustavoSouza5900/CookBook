namespace CookBook.Models;

public class Recipe {
    public int RecipeId { get; set; }
    public string Name { get; set; }
    public string Steps { get; set; }

    public int UserId { get; set; }
}

