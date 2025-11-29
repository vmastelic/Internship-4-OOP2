namespace Internship_4_OOP2.Application.DTOs.External
{
    public class ExternalUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string? Website { get; set; }
        public string CompanyName { get; set; } = null!;
    }
}
