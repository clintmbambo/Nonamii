using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nonamii.Models.UserDetails;

namespace Nonamii.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Extra> Extras { get; set; }
        public DbSet<Ingredient> Ingredients { get; set;}
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuItemIngredient> MenuItemIngredients { get; set; }
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
    }
}
