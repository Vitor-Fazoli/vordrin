namespace aegis_server.Models
{
    public class WeaponType
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Damage { get; set; }
        public string? Description { get; set; }
    }
}