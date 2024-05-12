namespace Nonamii.ViewModels
{
    public class CartViewModel
    {
        public Cart? Cart { get; set; }
        [Display(Name = "Self Collect or Delivery?")]
        public string? CollectOrDeliver { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
