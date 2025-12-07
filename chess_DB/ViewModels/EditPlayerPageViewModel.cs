using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using chess_DB.Models;
using chess_DB.Services;
using System;
using System.Threading.Tasks;

namespace chess_DB.ViewModels;

public partial class EditPlayerPageViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    private readonly PlayerService _playerService;

    // 🟦 Le joueur en cours d’édition
    private Player _player;

    // 🔹 Propriétés bindables
    [ObservableProperty] private string name;
    [ObservableProperty] private string surname;
    [ObservableProperty] private string gender;
    [ObservableProperty] private DateTime? birthdate;
    [ObservableProperty] private string email;
    [ObservableProperty] private string phone;
    [ObservableProperty] private string country;
    [ObservableProperty] private string city;
    [ObservableProperty] private string street;
    [ObservableProperty] private string postalCode;

    // 🔥 Constructeur : reçoit l’ID du joueur à modifier
    public EditPlayerPageViewModel(MainViewModel mainViewModel, Guid playerId)
    {
        _mainViewModel = mainViewModel;
        _playerService = new PlayerService();

        // Chargement des données du joueur
        _ = LoadPlayerAsync(playerId);
    }

    // 🔹 Charge le joueur depuis le JSON
    private async Task LoadPlayerAsync(Guid id)
    {
        _player = await _playerService.ObtenirJoueurParIdAsync(id)
                    ?? throw new Exception("Joueur introuvable.");

        // Copier les valeurs dans les propriétés bindées
        Name = _player.Name;
        Surname = _player.Surname;
        Gender = _player.Gender;
        Birthdate = _player.Birthdate;
        Email = _player.Email;
        Phone = _player.Phone;
        Country = _player.Country;
        City = _player.City;
        Street = _player.Street;
        PostalCode = _player.PostalCode;
    }

    // 🟢 Commande : Enregistrer les modifications
    [RelayCommand]
    private async Task SaveAsync()
    {
        // Mise à jour du modèle Player
        _player.Name = Name;
        _player.Surname = Surname;
        _player.Gender = Gender;
        _player.Birthdate = Birthdate;
        _player.Email = Email;
        _player.Phone = Phone;
        _player.Country = Country;
        _player.City = City;
        _player.Street = Street;
        _player.PostalCode = PostalCode;

        bool ok = await _playerService.ModifierJoueurAsync(_player);

        if (!ok)
        {
            Console.WriteLine("❌ Erreur lors de la modification du joueur.");
            return;
        }

        Console.WriteLine("✅ Joueur modifié avec succès.");

        // Retour à la liste
        _mainViewModel.CurrentPage = new ConsultPlayerPageViewModel(_mainViewModel);
    }

    // 🔵 Commande : Annuler
    [RelayCommand]
    private void Cancel()
    {
        _mainViewModel.CurrentPage = new ConsultPlayerPageViewModel(_mainViewModel);
    }
    
    [RelayCommand]
    private void GoToHomePage()
    {
        _mainViewModel.CurrentPage = new HomePageViewModel(_mainViewModel);
    }
}
