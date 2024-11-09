using CookBook.Models;
using Microsoft.EntityFrameworkCore;

public class CookBookDB : DbContext {
    public CookBookDB(DbContextOptions options) : base(options) {}
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<RecipeRead> RecipeReads { get; set; }

}