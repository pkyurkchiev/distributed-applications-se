namespace RestaurantClient.Models
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public DateTime RegisteredOn { get; set; }
    }
}
