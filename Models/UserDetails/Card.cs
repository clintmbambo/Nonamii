namespace Nonamii.Models.UserDetails
{
    public class Card
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        [Required]
        [Display(Name = "Cardholder Fullname")]
        public string? FullName { get; set; }
        [Required]
        [Display(Name = "Card Number")]
        public string? CardNumber { get; set; }
        [Required]
        public int CVV { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Card Expiry Date")]
        public DateOnly ExpiryDate { get; set; }
    }
}
