namespace chess_DB.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using chess_DB.Models;
using chess_DB.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;

public partial class AddCompetitionPageViewModel:ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

    public AddCompetitionPageViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }
    
    [RelayCommand]
    private void GoToHomePage()
    {
        _mainViewModel.CurrentPage = new HomePageViewModel(_mainViewModel);
    }
}