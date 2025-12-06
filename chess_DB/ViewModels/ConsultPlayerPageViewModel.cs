using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using chess_DB.Models;
using chess_DB.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;

namespace chess_DB.ViewModels;

public partial class ConsultPlayerPageViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    private readonly PlayerService _playerService;

    // 🔹 Liste observable → bindable dans XAML
    public ObservableCollection<Player> Players { get; } = new();

    public ConsultPlayerPageViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        _playerService = new PlayerService();

        // 🔄 Chargement automatique
        _ = LoadPlayersAsync();
    }

    // 🟦 Charger tous les joueurs
    [RelayCommand]
    public async Task LoadPlayersAsync()
    {
        Players.Clear();

        var players = await _playerService.ObtenirTousLesJoueursAsync();

        foreach (var p in players)
            Players.Add(p);
    }

    // 🟢 Bouton : Retour à l'accueil
    [RelayCommand]
    private void GoToHomePage()
    {
        _mainViewModel.CurrentPage = new HomePageViewModel(_mainViewModel);
    }

    // 🟠 Bouton : Aller à "Ajouter un joueur"
    [RelayCommand]
    private void GoToAddPlayerPage()
    {
        _mainViewModel.CurrentPage = new AddPlayerPageViewModel(_mainViewModel);
    }

    // 🔴 Supprimer un joueur
    [RelayCommand]
    private async Task DeletePlayerAsync(Player? player)
    {
        if (player is null)
            return;

        bool ok = await _playerService.SupprimerJoueurAsync(player.Id);

        if (!ok)
        {
            Console.WriteLine("❌ Erreur suppression.");
            return;
        }

        Players.Remove(player);  // Mise à jour UI
    }
}