using System;
using System.Collections.ObjectModel;
using System.Linq;
using chess_DB.Models;
using chess_DB.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace chess_DB.ViewModels
{
    public partial class ConsultCompetitionPageViewModel : ViewModelBase
    {
        private readonly CompetitionService _competitionService;
        private readonly MainViewModel _mainViewModel;

        public ObservableCollection<Competition> Competitions { get; } = new();
        public ObservableCollection<Competition> CompetitionsFiltered { get; } = new();

        [ObservableProperty]
        private string searchText = "";

        [ObservableProperty]
        private Competition? selectedCompetition;

        

        public ConsultCompetitionPageViewModel(MainViewModel mainViewModel,CompetitionService competitionService)
        {
            _mainViewModel = mainViewModel;
            _competitionService = competitionService;
            LoadCompetitions();
        }

        private async void LoadCompetitions()
        {
            Competitions.Clear();
            CompetitionsFiltered.Clear();

            var list = await _competitionService.ObtenirToutesLesCompetitionsAsync();

            foreach (var comp in list)
            {
                Competitions.Add(comp);
                CompetitionsFiltered.Add(comp);
            }
        }

        // 🔍 Filtrage automatique
        partial void OnSearchTextChanged(string value)
        {
            CompetitionsFiltered.Clear();

            var filtered = Competitions.Where(c =>
                c.Name.Contains(value, StringComparison.OrdinalIgnoreCase) ||
                c.Id.ToString().Contains(value) ||
                c.Type.Contains(value, StringComparison.OrdinalIgnoreCase) ||
                c.Place.Contains(value, StringComparison.OrdinalIgnoreCase)
            );

            foreach (var c in filtered)
                CompetitionsFiltered.Add(c);
        }
        
        [RelayCommand]
        private void GoToHomePage()
        {
            _mainViewModel.CurrentPage = new HomePageViewModel(_mainViewModel);
        }
    
        [RelayCommand]
        private void GoToCompetitionPage()
        {
            _mainViewModel.CurrentPage = new CompetitionPageViewModel(_mainViewModel);
        }
    }
    
    
}