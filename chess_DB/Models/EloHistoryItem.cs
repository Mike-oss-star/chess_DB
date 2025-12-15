using System;

namespace chess_DB.Models;

public class EloHistoryItem
{
    public DateTimeOffset Date { get; set; }
    public string Opponent { get; set; } = "";
    public string Result { get; set; } = "";
    public int EloBefore { get; set; }
    public int EloAfter { get; set; }
}
