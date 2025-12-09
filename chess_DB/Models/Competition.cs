using System;
using System.Collections.Generic;

namespace chess_DB.Models;

public class Competition
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<Guid> JoueursIds { get; set; } = new();
    public Competition()
    {
        Id = Guid.NewGuid();
    }
}