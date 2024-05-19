using Nonamii.Models.Promotions;

namespace Nonamii.Models.Inventory
{
    public class MenuItem
    {
        public int Id { get; set; }
        //Get restaurant.
        public string? UserId { get; set; }
        //Get recipe.
        public string? Image { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public List<MenuItemSize>? ItemSizes { get; set; }
        public List<MenuItemExtra>? ItemExtras { get; set; }
        public List<CartDetail>? CartDetails { get; set; }
        public List<MenuItemPromo>? MenuItemPromos { get; set; }
    }
}
