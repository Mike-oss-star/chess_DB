using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using chess_DB.Models;
using chess_DB.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace chess_DB.ViewModels;

public partial class AddGamePageViewModel : ViewModelBase
{
    private readonly GameService _gameService;
    private readonly PlayerService _playerService;
    private readonly CompetitionService _competitionService;

    // 🔵 Toutes les données
    private List<Player> _allPlayers = new();

    public List<string> Results { get; } = new()
    {
        "1-0",
        "1/2-1/2",
        "0-1"
    };

    // 🟢 Joueurs filtrés
    public ObservableCollection<Player> PlayersInCompetition { get; } = new();
    public ObservableCollection<Competition> Competitions { get; } = new();

    // Sélections
    [ObservableProperty] private Competition? selectedCompetition;
    [ObservableProperty] private Player? whitePlayer;
    [ObservableProperty] private Player? blackPlayer;

    // Champs
    [ObservableProperty] private string result = "1-0";
    [ObservableProperty] private string cadence = "classic";
    [ObservableProperty] private string moves = "";

    public AddGamePageViewModel()
    {
        _gameService = new GameService();
        _playerService = new PlayerService();
        _competitionService = new CompetitionService();

        LoadData();
    }

    private async void LoadData()
    {
        _allPlayers = await _playerService.ObtenirTousLesJoueursAsync();

        Competitions.Clear();
        foreach (var c in await _competitionService.ObtenirToutesLesCompetitionsAsync())
            Competitions.Add(c);
    }

    // 🔥 Quand la compétition change → filtrer les joueurs
    partial void OnSelectedCompetitionChanged(Competition? value)
    {
        PlayersInCompetition.Clear();

        if (value == null)
            return;

        var joueurs = _allPlayers
            .Where(p => value.JoueursIds.Contains(p.Id))
            .ToList();

        foreach (var j in joueurs)
            PlayersInCompetition.Add(j);

        // reset des sélections
        WhitePlayer = null;
        BlackPlayer = null;
    }

    // 💾 Sauvegarde
    [RelayCommand]
    private async Task SaveAsync()
    {
        if (SelectedCompetition == null ||
            WhitePlayer == null ||
            BlackPlayer == null ||
            WhitePlayer == BlackPlayer)
            return;

        // 1️⃣ Créer la partie
        var game = new Game
        {
            WhitePlayerId = WhitePlayer.Id,
            BlackPlayerId = BlackPlayer.Id,
            CompetitionId = SelectedCompetition.Id,
            Result = Result,
            Cadence = Cadence,
            Date = DateTime.Now,
            Moves = Moves // si tu as ajouté les coups
        };

        // 2️⃣ Sauvegarder la partie
        bool saved = await _gameService.AddAsync(game);
        if (!saved)
            return;

        // 3️⃣ Ajouter l’ID de la partie à la compétition
        SelectedCompetition.GameIds.Add(game.Id);

        // 4️⃣ Sauvegarder la compétition modifiée
        await _competitionService.ModifierCompetitionAsync(SelectedCompetition);

        // 5️⃣ Reset formulaire
        WhitePlayer = null;
        BlackPlayer = null;
        SelectedCompetition = null;
        Result = "1-0";
        Cadence = "Classique";
        Moves = "";
    }

}
