using System;
using System.Collections.Generic;

namespace chess_DB.Models;

public class Competition
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<PlayerCompetition> Subscriptions { get; set; } = new();
}