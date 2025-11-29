namespace Application.DTOs.Users
{
    public class UpdateUserDto
    {
        public string Name { get; set; } = null!;
        public string AddressStreet { get; set; } = null!;
        public string AddressCity { get; set; } = null!;
        public string ?Website { get; set; }
        public double GeoLat { get; set; }
        public double GeoLng { get; set; }
    }
}
