using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using chess_DB.Models;
using chess_DB.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace chess_DB.ViewModels;

public partial class RemovePlayerPageViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    private readonly PlayerService _playerService;

    public ObservableCollection<Player> Players { get; } = new();

    [ObservableProperty]
    private Player? selectedPlayer;

    [ObservableProperty]
    private string searchId = "";

    public RemovePlayerPageViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        _playerService = new PlayerService();

        LoadPlayers();
    }

    private async void LoadPlayers()
    {
        try
        {
            var joueurs = await _playerService.ObtenirTousLesJoueursAsync();
            Players.Clear();
            foreach (var j in joueurs)
                Players.Add(j);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur LoadPlayers: {ex.Message}");
        }
    }

    // 🔵 Commande supprimer le joueur sélectionné
    [RelayCommand]
    private async Task RemoveAsync()
    {
        if (SelectedPlayer != null)
        {
            bool ok = await _playerService.SupprimerJoueurAsync(SelectedPlayer.Id);
            if (ok)
            {
                Players.Remove(SelectedPlayer);
                SelectedPlayer = null;
            }
        }
    }

    // 🔵 Commande rechercher par ID
    [RelayCommand]
    private async Task SearchByIdAsync()
    {
        if (Guid.TryParse(SearchId, out Guid id))
        {
            var joueur = await _playerService.ObtenirJoueurParIdAsync(id);
            Players.Clear();
            if (joueur != null)
                Players.Add(joueur);
        }
    }

    //  Commande réinitialiser la recherche
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
