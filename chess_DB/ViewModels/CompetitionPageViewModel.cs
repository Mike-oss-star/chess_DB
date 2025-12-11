namespace chess_DB.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using chess_DB.Models;
using chess_DB.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;

public partial class CompetitionPageViewModel:ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

    public CompetitionPageViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }
    
    [RelayCommand]
    private void GoToAddCompetitionPage()
    {
        _mainViewModel.CurrentPage = new AddCompetitionPageViewModel(_mainViewModel,new CompetitionService(),new PlayerService());
    }
    
    [RelayCommand]
    private void GoToConsultCompetitionPage()
    {
        //_mainViewModel.CurrentPage = new ConsultPlayerPageViewModel(_mainViewModel);
    }

    [RelayCommand]
    private void GoToHomePage()
    {
        _mainViewModel.CurrentPage = new HomePageViewModel(_mainViewModel);
    }
    
    [RelayCommand]
    private void GoToEditCompetitionPage()
    {
        _mainViewModel.CurrentPage = new EditCompetitionPageViewModel(_mainViewModel);
    }
    
    [RelayCommand]
    private void GoToRemoveCompetitionPage()
    {
        _mainViewModel.CurrentPage = new RemoveCompetitionPageViewModel(new CompetitionService());
    }
}