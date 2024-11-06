namespace CookBook.Models;

public class RecipeRead {
    public int RecipeId { get; set; }
    public int UserId { get; set; }
    public DateTime ViwedDate { get; set; }
    public int StarsRating { get; set; }
    public bool Favorite { get; set; }

    public Recipe Recipe { get; set; }
    public User User { get; set; }
}