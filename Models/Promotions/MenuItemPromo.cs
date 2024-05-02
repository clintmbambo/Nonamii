namespace Nonamii.Models.Promotions
{
    public class MenuItemPromo
    {
        public int Id { get; set; }
        [Display(Name = "Promotion")]
        public int PromotionId { get; set; }
        [Display(Name = "Item")]
        public int MenuItemId { get; set; }
    }
}
