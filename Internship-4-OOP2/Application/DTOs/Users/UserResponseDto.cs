namespace Application.DTOs.Users
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public string AdressStreet { get; internal set; } = null!;
        public string AdressCity { get; internal set; } = null!;
        public string? Website { get; set; }
        public decimal GeoLat { get; set; }
        public decimal GeoLng { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
