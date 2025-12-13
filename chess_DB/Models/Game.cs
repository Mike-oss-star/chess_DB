using System;

namespace chess_DB.Models;

public class Game
{
    public Guid Id { get; set; }

    public Guid WhitePlayerId { get; set; }
    public Guid BlackPlayerId { get; set; }

    public Guid? CompetitionId { get; set; }

    public string Result { get; set; } = "";
    public string Cadence { get; set; } = "";

    public DateTime Date { get; set; }

    public Game()
    {
        Id = Guid.NewGuid();
    }
}