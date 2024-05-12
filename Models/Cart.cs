namespace Nonamii.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public bool IsActive { get; set; } = false;

        public List<CartDetail>? Details { get; set; }

    }
}
