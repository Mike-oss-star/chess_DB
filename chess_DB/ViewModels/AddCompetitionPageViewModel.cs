using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using chess_DB.Models;
using chess_DB.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace chess_DB.ViewModels;

public partial class AddCompetitionPageViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    private readonly CompetitionService _competitionService;
    private readonly PlayerService _playerService;

    public AddCompetitionPageViewModel(
        MainViewModel mainViewModel,
        CompetitionService competitionService,
        PlayerService playerService)
    {
        _mainViewModel = mainViewModel;
        _competitionService = competitionService;
        _playerService = playerService;

        ChargerJoueurs();
    }

    public List<string> Types { get; } = new()
    {
        "open Tournament",
        "Championship",
        "By teams",
        "Qualifying Tournament",
        "Friendly Tournament",
        "Giant Blitz"
    };

    public List<string> Systems { get; } = new()
    {
        "Swiss System",
        "Round Robin",
        "Knocked Out",
        "Double elimination",
        "By Team System",
        "Hybrid System"
    };

    public List<string> Cadences { get; } = new()
    {
        "Classic",
        "Fast",
        "Blitz",
        "Bullet",
        "Fischer",
        "Mixt Cadence"
    };

    public List<string> Categories { get; } = new()
    {
        "Young",
        "Female",
        "Senior",
        "Mixt"
    };

    // 🟦 Propriétés bindables
    [ObservableProperty] private string type = "";
    [ObservableProperty] private string name = "";
    [ObservableProperty] private string place = "";
    [ObservableProperty] private DateTimeOffset? startDate;
    [ObservableProperty] private DateTimeOffset? endDate;
    [ObservableProperty] private string system = "";
    [ObservableProperty] private string cadence = "";
    [ObservableProperty] private string rule = "";
    [ObservableProperty] private string category = "";
    [ObservableProperty] private int capacity = 0;

    // 🟦 Liste des joueurs pour le MultiSelect
    public ObservableCollection<Player> Players { get; } = new();

    // 🟦 Joueurs sélectionnés
    [ObservableProperty] private List<Player> selectedPlayers = new();

    // 🔵 Charge tous les joueurs existants
    private async void ChargerJoueurs()
    {
        var joueurs = await _playerService.ObtenirTousLesJoueursAsync();
        Players.Clear();

        foreach (var j in joueurs)
            Players.Add(j);
    }

    // 🔵 Navigation
    [RelayCommand]
    private void GoToHomePage()
    {
        _mainViewModel.CurrentPage = new HomePageViewModel(_mainViewModel);
    }

    // 🟢 Enregistrement
    [RelayCommand]
    private async Task SaveAsync()
    {
        var competition = new Competition
        {
            Type = Type,
            Name = Name,
            Place = Place,
            StartDate = StartDate?.DateTime ?? DateTime.Now,
            EndDate = EndDate?.DateTime ?? DateTime.Now,
            System = System,
            Cadence = Cadence,
            Rule = Rule,
            Category = Category,
            Capacity = Capacity,
            JoueursIds = new List<Guid>()
        };

        // Ajout des joueurs choisis
        foreach (var player in SelectedPlayers)
            competition.JoueursIds.Add(player.Id);

        bool ok = await _competitionService.AjouterCompetitionAsync(competition);

        if (ok)
        {
            // Retour à la page de consultation des compétitions
           // _mainViewModel.CurrentPage = new ConsultCompetitionPageViewModel(_mainViewModel, _competitionService);
        }
    }


    
    
}