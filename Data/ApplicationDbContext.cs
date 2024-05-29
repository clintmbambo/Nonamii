using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nonamii.Models.UserDetails;
using Nonamii.Models.Inventory;
using Nonamii.Models;

namespace Nonamii.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Extra> Extras { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuItemExtra> MenuItemExtras { get; set; }
        public DbSet<MenuItemSize> MenuItemSizes { get; set; }
        public DbSet<Size> Sizes { get;set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartsDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderDetail> OrdersDetails { get; set;}
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        public DbSet<Nonamii.Models.UserDetails.Address> Address { get; set; } = default!;
        public DbSet<Nonamii.Models.UserDetails.Card> Card { get; set; } = default!;
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Nonamii.Models.Inventory.Menu> Menu { get; set; } = default!;
        public DbSet<Nonamii.Models.Inventory.MenuType> MenuType { get; set; } = default!;
        public DbSet<Nonamii.Models.Inventory.Category> Category { get; set; } = default!;
        public DbSet<Nonamii.Models.Inventory.MenuCategory> MenuCategory { get; set; } = default!;
        public DbSet<Nonamii.Models.Inventory.Measurement> Measurement { get; set; } = default!;
        public DbSet<Nonamii.Models.Inventory.Ingredient> Ingredients { get; set; } = default!;
        public DbSet<Nonamii.Models.Restaurant> Restaurant { get; set; } = default!;
        public DbSet<Nonamii.Models.Delivery> Delivery { get; set; } = default!;
    }
}
