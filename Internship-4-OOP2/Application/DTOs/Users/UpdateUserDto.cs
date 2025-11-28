namespace Application.DTOs.Users
{
    public class UpdateUserDto
    {
        public string Name { get; set; } = null!;
        public string AddressStreet { get; set; } = null!;
        public string AddressCity { get; set; } = null!;
        public string ?Website { get; set; }
        public decimal GeoLat { get; set; }
        public decimal GeoLng { get; set; }
    }
}
