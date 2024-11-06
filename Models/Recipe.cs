namespace CookBook.Models;

public class Recipe {
    public Recipe (string Name, string Steps, string ImagePath){
        this.Name = Name;
        this.Steps = Steps;
        this.ImagePath = ImagePath;
    }

    public int RecipeId { get; set; }
    public string Name { get; set; }
    public string Steps { get; set; }

    public float Stars { get; set; }

    public string ImagePath { get; set; }

    public int UserId { get; set; }
}

