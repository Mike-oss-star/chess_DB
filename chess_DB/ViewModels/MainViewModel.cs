using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace chess_DB.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private ViewModelBase _currentPage;

    public bool HomePageIsActive => CurrentPage == _homePage;

    private readonly HomePageViewModel _homePage;
   

    public MainViewModel()
    {
        _homePage = new HomePageViewModel(this);
        

        CurrentPage = _homePage; // page par défaut
    }

    [RelayCommand]
    public void GoToHomePage()
    {
        CurrentPage = _homePage;
    }

 
    
}