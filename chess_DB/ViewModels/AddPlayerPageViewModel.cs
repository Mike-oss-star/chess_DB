using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using chess_DB.Models;
using chess_DB.Services;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace chess_DB.ViewModels;

public partial class AddPlayerPageViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    private readonly PlayerService _playerService;

    // 🔹 Constructeur
    public AddPlayerPageViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        _playerService = new PlayerService(); // ⚠️ Service instancié ici
    }
    
    public List<string> Genders { get; } = new()
    {
        "Male",
        "Female",
        "Other"
    };

    // 🟦 Champs bindables
    [ObservableProperty] private string name = "";
    [ObservableProperty] private string surname = "";
    [ObservableProperty] private string gender = "";
    [ObservableProperty] private DateTimeOffset? birthdate;
    [ObservableProperty] private string email = "";
    [ObservableProperty] private string phone = "";
    [ObservableProperty] private string country = "";
    [ObservableProperty] private string city = "";
    [ObservableProperty] private string street = "";
    [ObservableProperty] private string postalCode = "";

    // 🔵 Commande : Retour à l'accueil
    [RelayCommand]
    private void GoToHomePage()
    {
        _mainViewModel.CurrentPage = new HomePageViewModel(_mainViewModel);
    }

    // 🟢 Commande : Enregistrer (version asynchrone)
    [RelayCommand]
    private async Task SaveAsync()
    {
        var newPlayer = new Player
        {
            Id = Guid.NewGuid(),
            Name = Name,
            Surname = Surname,
            Gender = Gender,
            Birthdate = Birthdate,
            Email = Email,
            Phone = Phone,
            Country = Country,
            City = City,
            Street = Street,
            PostalCode = PostalCode
        };

        bool ok = await _playerService.AjouterJoueurAsync(newPlayer);

        if (!ok)
        {
            Console.WriteLine("❌ Erreur lors de l'enregistrement du joueur.");
            return;
        }

        Console.WriteLine("✅ Joueur enregistré avec succès.");

        // Retour à la liste (si tu veux)
        //_mainViewModel.CurrentPage = new ConsultPlayerPageViewModel(_mainViewModel);

        // Ou retour à l'accueil :
        _mainViewModel.CurrentPage = new HomePageViewModel(_mainViewModel);
    }
    
}
