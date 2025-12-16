using CommunityToolkit.Mvvm.ComponentModel;
using chess_DB.Models;
using chess_DB.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace chess_DB.ViewModels;

public partial class ConsultPlayerPageViewModel : ViewModelBase
{
    
    private readonly PlayerService _playerService = new();

    public ObservableCollection<Player> Players { get; } = new();

    [ObservableProperty]
    private Player? selectedPlayer;

    
    private readonly MainViewModel _mainViewModel;

    public ConsultPlayerPageViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        LoadAsync();
    }

    private async Task LoadAsync()
    {
        var players = await _playerService.ObtenirTousLesJoueursAsync();
        Players.Clear();
        players.ForEach(p => Players.Add(p));
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