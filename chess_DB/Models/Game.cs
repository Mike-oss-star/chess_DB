namespace chess_DB.Models;

public class Game
{
    public int Id { get; set; }
    public int CompetitionId { get; set; }
    public Competition? Competition { get; set; }
    public int PlayerWhiteId { get; set; }
    public Player? PlayerWhite { get; set; }
    public int PlayerBlackId { get; set; }
    public Player? PlayerBlack { get; set; }
    public string Coups { get; set; } = string.Empty;
    public string Result { get; set; } = string.Empty;
}