namespace chess_DB.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

public partial class PlayerPageViewModel:ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

    public PlayerPageViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }
    
    [RelayCommand]
    private void GoToAddPlayerPage()
    {
        _mainViewModel.CurrentPage = new AddPlayerPageViewModel(_mainViewModel);
    }
    
    [RelayCommand]
    private void GoToConsultPlayerPage()
    {
        _mainViewModel.CurrentPage = new ConsultPlayerPageViewModel(_mainViewModel);
    }
    
    [RelayCommand]
    private void GoToHomePage()
    {
        _mainViewModel.CurrentPage = new HomePageViewModel(_mainViewModel);
    }
}