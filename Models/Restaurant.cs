namespace Nonamii.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string? UserId { get; set; }

        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
    }
}
