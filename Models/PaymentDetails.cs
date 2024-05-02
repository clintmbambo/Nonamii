namespace Nonamii.Models
{
    public class PaymentDetails
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public long CardNumber { get; set; }
        public long CVV { get; set; }
        public string? CardHolderName { get; set; }
        public string? Region { get; set; }
    }
}
