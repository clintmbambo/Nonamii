namespace Nonamii.Models.UserDetails
{
    public class Address
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Street { get; set; }
        public string? ZIP { get; set; }
        public string? Building { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
    }
}
