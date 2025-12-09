using System;

namespace chess_DB.Models;

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTimeOffset? Birthdate { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }
    public int EloRanking { get; set; }
    
    public Player()
    {
        Id = Guid.NewGuid(); // Génère un ID unique
    }
}