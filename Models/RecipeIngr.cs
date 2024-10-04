namespace CookBook.Models;

public class RecipeIngr {
    public int RecipeId { get; set; }
    public int IngredientId { get; set; }
    public float Quantity { get; set; }

    public Recipe Recipe { get; set; }
    public Ingredient Ingredient { get; set; }
}