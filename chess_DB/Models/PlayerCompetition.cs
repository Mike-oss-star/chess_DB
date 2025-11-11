namespace chess_DB.Models;

public class PlayerCompetition
{
    public int PlayerId { get; set; }
    public Player? Player { get; set; }
    public int CompetitionId { get; set; }
    public Competition? Competition { get; set; }
}