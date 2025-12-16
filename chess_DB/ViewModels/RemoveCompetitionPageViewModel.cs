using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using chess_DB.Models;
using chess_DB.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace chess_DB.ViewModels
{
    public partial class RemoveCompetitionPageViewModel : ViewModelBase
    {
        private readonly CompetitionService _competitionService;
        private readonly MainViewModel _mainViewModel;

        [ObservableProperty]
        private string searchText = "";

        [ObservableProperty]
        private Competition? selectedCompetition;

        public ObservableCollection<Competition> Competitions { get; set; } = new();
        public ObservableCollection<Competition> FilteredCompetitions { get; set; } = new();

        public RemoveCompetitionPageViewModel(MainViewModel mainViewModel, CompetitionService competitionService)
        {
            _mainViewModel= mainViewModel;
            _competitionService = competitionService;
            LoadCompetitions();
        }

        private async void LoadCompetitions()
        {
            Competitions.Clear();
            FilteredCompetitions.Clear();

            var all = await _competitionService.ObtenirToutesLesCompetitionsAsync();

            foreach (var comp in all)
            {
                Competitions.Add(comp);
                FilteredCompetitions.Add(comp);
            }
        }

        // Recherche
        partial void OnSearchTextChanged(string value)
        {
            FilteredCompetitions.Clear();

            var filtered = Competitions.Where(c =>
                c.Name.Contains(value, StringComparison.OrdinalIgnoreCase) ||
                c.Id.ToString().Contains(value));

            foreach (var c in filtered)
                FilteredCompetitions.Add(c);
        }

        // 🔴 Commander la suppression
        [RelayCommand]
        private async Task DeleteAsync()
        {
            if (SelectedCompetition == null)
                return;

            await _competitionService.SupprimerCompetitionAsync(SelectedCompetition.Id);

            LoadCompetitions();
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
