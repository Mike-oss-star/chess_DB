using chess_DB.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace chess_DB.ViewModels;

public partial class HomePageViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

    public HomePageViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    [RelayCommand]
    private void GoToPlayerPage()
    {
        _mainViewModel.CurrentPage = new PlayerPageViewModel(_mainViewModel);
    }

    [RelayCommand]
    private void GoToCompetitionPage()
    {
        _mainViewModel.CurrentPage = new CompetitionPageViewModel(_mainViewModel);
    }
    
    [RelayCommand]
    private void GoToGamePage()
    {
        _mainViewModel.CurrentPage = new EditGamePageViewModel();
    }
}