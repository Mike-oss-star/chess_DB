using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using chess_DB.Models;
using chess_DB.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace chess_DB.ViewModels;

public partial class ConsultGamePageViewModel : ViewModelBase
{
    private readonly GameService _gameService;
    private readonly PlayerService _playerService;
    private readonly CompetitionService _competitionService;

    public ObservableCollection<Competition> Competitions { get; } = new();
    public ObservableCollection<Game> Games { get; } = new();
    public List<Player> AllPlayers { get; private set; } = new();
    public List<Competition> AllCompetitions { get; private set; } = new();

    [ObservableProperty] private Competition? selectedCompetition;
    [ObservableProperty] private Game? selectedGame;

    public ConsultGamePageViewModel()
    {
        _gameService = new GameService();
        _playerService = new PlayerService();
        _competitionService = new CompetitionService();

        LoadData();
    }

    private async void LoadData()
    {
        AllPlayers = await _playerService.ObtenirTousLesJoueursAsync();
        AllCompetitions = await _competitionService.ObtenirToutesLesCompetitionsAsync();

        Competitions.Clear();
        foreach (var c in AllCompetitions)
            Competitions.Add(c);
    }

    partial void OnSelectedCompetitionChanged(Competition? value)
    {
        LoadGamesForCompetition();
    }

    partial void OnSelectedGameChanged(Game? value)
    {
        // On peut mettre à jour les bindings calculés automatiquement
        OnPropertyChanged(nameof(WhitePlayerName));
        OnPropertyChanged(nameof(BlackPlayerName));
        OnPropertyChanged(nameof(CompetitionName));
    }

    private async void LoadGamesForCompetition()
    {
        Games.Clear();
        if (SelectedCompetition == null) return;

        var allGames = await _gameService.GetAllAsync();
        foreach (var g in allGames.Where(g => g.CompetitionId == SelectedCompetition.Id))
        {
            Games.Add(g);
        }

        SelectedGame = null;
    }

    // 🔹 Propriétés calculées pour la vue
    public string WhitePlayerName => SelectedGame == null 
        ? "" 
        : AllPlayers.FirstOrDefault(p => p.Id == SelectedGame.WhitePlayerId)?.Name ?? "Inconnu";

    public string BlackPlayerName => SelectedGame == null 
        ? "" 
        : AllPlayers.FirstOrDefault(p => p.Id == SelectedGame.BlackPlayerId)?.Name ?? "Inconnu";

    public string CompetitionName => SelectedGame == null 
        ? "" 
        : AllCompetitions.FirstOrDefault(c => c.Id == SelectedGame.CompetitionId)?.Name ?? "Inconnu";
}
