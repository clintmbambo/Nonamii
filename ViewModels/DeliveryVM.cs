namespace Nonamii.ViewModels
{
    public class DeliveryVM
    {
        //Customer details.
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        //Destination
        public string? CustomerAddress { get; set; }

        public string? RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
        //From
        public string? RestaurantAddress { get; set; }

        public int OrderId { get; set; }
        public decimal PackageWeight { get; set; }
    }
}
