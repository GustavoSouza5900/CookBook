namespace CookBook.Models;

public class RecipeRead {

    public RecipeRead() { }

    public RecipeRead(int RecipeId, int UserId, Recipe recipe, User user){
        this.RecipeId = RecipeId;
        this.UserId = UserId;
        this.Recipe = recipe;
        this.User = user;
    }

    public int RecipeReadId { get; set;}
    public int RecipeId { get; set; }
    public int UserId { get; set; }
    public int StarsRating { get; set; }
    public bool Favorite { get; set; }
    public Recipe Recipe { get; set; }
    public User User { get; set; }
}