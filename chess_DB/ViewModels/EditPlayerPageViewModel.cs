using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using chess_DB.Models;
using chess_DB.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace chess_DB.ViewModels;

public partial class EditPlayerPageViewModel : ViewModelBase
{
    
    private readonly MainViewModel _mainViewModel;
    
    private readonly PlayerService _playerService;

    // Liste de tous les joueurs
    public ObservableCollection<Player> Players { get; } = new();

    // Joueur actuellement sélectionné
    [ObservableProperty]
    private Player? selectedPlayer;

    // Barre de recherche (ID)
    [ObservableProperty]
    private string searchId = "";

    public EditPlayerPageViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        _playerService = new PlayerService();

        LoadPlayers();
    }

    // Charger tous les joueurs dans Players
    private async void LoadPlayers()
    {
        var joueurs = await _playerService.ObtenirTousLesJoueursAsync();
        Players.Clear();
        foreach (var j in joueurs)
            Players.Add(j);
    }

    // 🔵 Commande pour sauvegarder le joueur sélectionné
    [RelayCommand]
    private async Task SaveAsync()
    {
        if (SelectedPlayer != null)
        {
            bool ok = await _playerService.ModifierJoueurAsync(SelectedPlayer);
            if (ok)
            {
                // Recharge la liste après modification
                LoadPlayers();
            }
        }
    }

    //  Commande pour rechercher un joueur par ID
    [RelayCommand]
    private async Task SearchByIdAsync()
    {
        if (Guid.TryParse(SearchId, out Guid id))
        {
            var joueur = await _playerService.ObtenirJoueurParIdAsync(id);
            if (joueur != null)
            {
                SelectedPlayer = joueur;

                // Filtrer la liste pour ne montrer que ce joueur
                Players.Clear();
                Players.Add(joueur);
            }
        }
    }

    //  Commande pour réinitialiser la recherche
    [RelayCommand]
    private void ResetSearch()
    {
        SearchId = "";
        LoadPlayers();
    }
    
    [RelayCommand]
    private void GoToHomePage()
    {
        _mainViewModel.CurrentPage = new HomePageViewModel(_mainViewModel);
    }
    
    [RelayCommand]
    private void GoToPlayerPage()
    {
        _mainViewModel.CurrentPage = new PlayerPageViewModel(_mainViewModel);
    }
}
