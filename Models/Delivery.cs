namespace Nonamii.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        //Get order
        public int Key { get; set; }
        //Get Restaurant details
        public string? RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
        public string? RestaurantAddress { get; set; }

        //Get Customer Address
        public string? CustomerAddress { get; set; }

        public decimal PackageWeight { get; set; }
    }
}
