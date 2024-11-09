namespace CookBook.Models;

public class Comment {
    public int CommentId { get; set; }
    public string Content { get; set; }

    // Id of the 'parent' a recipe or another comment
    public int ParentId { get; set; }
    // Id of the author
    public int UserId { get; set; }

    public string UserName { get; set; }
}