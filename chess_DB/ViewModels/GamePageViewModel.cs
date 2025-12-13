namespace chess_DB.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using chess_DB.Services;

public partial class GamePageViewModel:ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

    public GamePageViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }
    
    [RelayCommand]
    private void GoToAddGamePage()
    {
        _mainViewModel.CurrentPage = new AddGamePageViewModel();
    }
    
    [RelayCommand]
    private void GoToConsultGamePage()
    {
        _mainViewModel.CurrentPage = new ConsultGamePageViewModel();
    }
    
    [RelayCommand]
    private void GoToHomePage()
    {
        _mainViewModel.CurrentPage = new HomePageViewModel(_mainViewModel);
    }
    [RelayCommand]
    private void GoToEditGamePage()
    {
        _mainViewModel.CurrentPage = new EditGamePageViewModel();
    }
    
    [RelayCommand]
    private void GoToRemoveGamePage()
    {
        _mainViewModel.CurrentPage = new RemovePlayerPageViewModel(_mainViewModel);
    }
}