using CookBook.Models;
using Microsoft.EntityFrameworkCore;

public class CookBookDB : DbContext {
    public CookBookDB(DbContextOptions options) : base(options) {}
    public DbSet<Recipe> Recipes { get; set; }

}