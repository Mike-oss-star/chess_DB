using System;
using System.Collections.ObjectModel;
using System.Linq;
using chess_DB.Models;
using chess_DB.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace chess_DB.ViewModels;

public partial class PlayerEloHistoryPageViewModel : ViewModelBase
{
    private readonly PlayerService _playerService;
    private readonly GameService _gameService;
    private readonly MainViewModel _mainViewModel;

    public ObservableCollection<Player> Players { get; } = new();
    public ObservableCollection<EloHistoryItem> EloHistory { get; } = new();

    [ObservableProperty] private Player? selectedPlayer;

    public PlayerEloHistoryPageViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        _playerService = new PlayerService();
        _gameService = new GameService();

        LoadPlayers();
    }

    private async void LoadPlayers()
    {
        var players = await _playerService.ObtenirTousLesJoueursAsync();
        foreach (var p in players)
            Players.Add(p);
    }

    partial void OnSelectedPlayerChanged(Player? value)
    {
        LoadEloHistory();
    }

    private async void LoadEloHistory()
    {
        EloHistory.Clear();
        if (SelectedPlayer == null) return;

        var games = (await _gameService.GetAllAsync())
            .Where(g => g.WhitePlayerId == SelectedPlayer.Id || g.BlackPlayerId == SelectedPlayer.Id)
            .OrderBy(g => g.Date)
            .ToList();

        int currentElo = 1200;

        foreach (var game in games)
        {
            bool isWhite = game.WhitePlayerId == SelectedPlayer.Id;
            int opponentId = isWhite ? game.BlackPlayerId.GetHashCode() : game.WhitePlayerId.GetHashCode();

            double expected = isWhite
                ? 1 / (1 + Math.Pow(10, (GetOpponentElo(game, isWhite) - currentElo) / 400.0))
                : 1 / (1 + Math.Pow(10, (GetOpponentElo(game, isWhite) - currentElo) / 400.0));

            double score = game.Result switch
            {
                "1-0" => isWhite ? 1 : 0,
                "0-1" => isWhite ? 0 : 1,
                "1/2-1/2" => 0.5,
                _ => 0
            };

            int newElo = (int)(currentElo + 20 * (score - expected));

            EloHistory.Add(new EloHistoryItem
            {
                Date = game.Date,
                Opponent = GetOpponentName(game, isWhite),
                Result = game.Result,
                EloBefore = currentElo,
                EloAfter = newElo
            });

            currentElo = newElo;
        }
    }

    private int GetOpponentElo(Game game, bool isWhite)
    {
        var opponentId = isWhite ? game.BlackPlayerId : game.WhitePlayerId;
        return Players.FirstOrDefault(p => p.Id == opponentId)?.Elo ?? 1200;
    }

    private string GetOpponentName(Game game, bool isWhite)
    {
        var opponentId = isWhite ? game.BlackPlayerId : game.WhitePlayerId;
        return Players.FirstOrDefault(p => p.Id == opponentId)?.Name ?? "Inconnu";
    }
    
    [RelayCommand]
    private void GoToHomePage()
    {
        _mainViewModel.CurrentPage = new HomePageViewModel(_mainViewModel);
    }
}
