using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using chess_DB.Models;
using chess_DB.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace chess_DB.ViewModels;

public partial class EloRankingPageViewModel : ViewModelBase
{
    private readonly PlayerService _playerService;
    private readonly GameService _gameService;

    public ObservableCollection<PlayerRankingItem> Rankings { get; } = new();

    public EloRankingPageViewModel()
    {
        _playerService = new PlayerService();
        _gameService = new GameService();

        LoadRanking();
    }

    private async void LoadRanking()
    {
        Rankings.Clear();

        var players = await _playerService.ObtenirTousLesJoueursAsync();
        var games = await _gameService.GetAllAsync();

        var temp = new List<PlayerRankingItem>();

        foreach (var player in players)
        {
            int wins = 0;
            int losses = 0;

            foreach (var g in games)
            {
                if (g.WhitePlayerId == player.Id)
                {
                    if (g.Result == "1-0") wins++;
                    else if (g.Result == "0-1") losses++;
                }
                else if (g.BlackPlayerId == player.Id)
                {
                    if (g.Result == "0-1") wins++;
                    else if (g.Result == "1-0") losses++;
                }
            }

            temp.Add(new PlayerRankingItem
            {
                PlayerName = player.Name,
                Elo = player.Elo,
                Wins = wins,
                Losses = losses
            });
        }

        // 🔥 Tri par Elo décroissant
        var ordered = temp.OrderByDescending(r => r.Elo).ToList();

        // 🔢 Attribution des rangs
        for (int i = 0; i < ordered.Count; i++)
        {
            ordered[i].Rank = i + 1;
            Rankings.Add(ordered[i]);
        }
    }

}