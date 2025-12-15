namespace chess_DB.Models;

public class PlayerRankingItem
{
    public int Rank { get; set; }
    public string PlayerName { get; set; } = "";
    public int Elo { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
}
