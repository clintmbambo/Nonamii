namespace Nonamii.Models.Promotions
{
    public class Promotion
    {
        public int Id { get; set; }
        public string? UserId { get; set; }

        public string? Description { get; set; }
        public DateTime StartDate { get;set; }
        public DateTime EndDate { get;set; }
        public decimal? Price { get;set; }

        public List<MenuItemPromo>? PromoItems { get; set; }
    }
}
