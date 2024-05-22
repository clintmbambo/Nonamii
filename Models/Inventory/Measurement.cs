namespace Nonamii.Models.Inventory
{
    public class Measurement
    {
        public int Id { get; set; }
        //get restaurant whose measurements a measurement belongs
        public string? UserId { get; set; }

        public string? Title { get; set; }
        public string? Abbreviation { get; set; }
    }
}
