using chess_DB.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace chess_DB.Services;

public partial class PlayerSelectable : ObservableObject
{
    public Player Player { get; }

    [ObservableProperty]
    private bool isSelected;

    public string Name => Player.Name;

    public PlayerSelectable(Player player)
    {
        Player = player;
    }
}
