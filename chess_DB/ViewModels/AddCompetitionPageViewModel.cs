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

    // 🟦 Propriétés bindables
    [ObservableProperty] private string name = "";
    [ObservableProperty] private string description = "";
    [ObservableProperty] private DateTimeOffset? startDate;
    [ObservableProperty] private DateTimeOffset? endDate;

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
            Name = Name,
            Description = Description,
            StartDate = StartDate?.DateTime ?? DateTime.Now,
            EndDate = EndDate?.DateTime ?? DateTime.Now,
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