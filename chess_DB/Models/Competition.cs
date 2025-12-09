using System;
using System.Collections.Generic;

namespace chess_DB.Models;

public class Competition
{
    public Guid Id { get; set; }
    
    public string Type {get; set;}
    public string Name { get; set; }
    public string Place { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string System { get; set; }
    public string Cadence { get; set; }
    public string Rule { get; set; }
    public string Category { get; set; }
    public int Capacity { get; set; }
    public List<Guid> JoueursIds { get; set; } = new();
    public Competition()
    {
        Id = Guid.NewGuid();
    }
}