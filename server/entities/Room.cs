using System.ComponentModel.DataAnnotations;

namespace Aegis.Entities;

public class Room(Guid id, List<Player> players)
{
    public Guid Id { get; set; } = id;

    [Required]
    public List<Player> Players { get; set; } = players;
}