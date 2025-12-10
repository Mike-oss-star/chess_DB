using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using chess_DB.Models;
using chess_DB.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace chess_DB.ViewModels
{
    public partial class EditCompetitionPageViewModel : ViewModelBase
    {
        private readonly MainViewModel _mainViewModel;
        private readonly CompetitionService _competitionService;

        // Liste des compétitions
        public ObservableCollection<Competition> Competitions { get; } = new();

        // Compétition sélectionnée
        [ObservableProperty]
        private Competition? selectedCompetition;

        // Champs éditables
        [ObservableProperty] private string type;
        [ObservableProperty] private string name;
        [ObservableProperty] private string place;
        [ObservableProperty] private DateTimeOffset startDate;
        [ObservableProperty] private DateTimeOffset endDate;
        [ObservableProperty] private string system;
        [ObservableProperty] private string cadence;
        [ObservableProperty] private string rule;
        [ObservableProperty] private string category;
        [ObservableProperty] private int capacity;

        public EditCompetitionPageViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _competitionService = new CompetitionService();

            LoadCompetitions();
        }

        // Charger toutes les compétitions
        private async void LoadCompetitions()
        {
            Competitions.Clear();
            var all = await _competitionService.ObtenirToutesLesCompetitionsAsync();
            foreach (var c in all)
                Competitions.Add(c);
        }

        // Quand on sélectionne une compétition → remplir le formulaire
        partial void OnSelectedCompetitionChanged(Competition? value)
        {
            if (value == null) return;

            Type = value.Type;
            Name = value.Name;
            Place = value.Place;
            StartDate = value.StartDate;
            EndDate = value.EndDate;
            System = value.System;
            Cadence = value.Cadence;
            Rule = value.Rule;
            Category = value.Category;
            Capacity = value.Capacity;
        }

        // Sauvegarder les modifications
        [RelayCommand]
        private async Task SaveAsync()
        {
            if (SelectedCompetition == null)
                return;

            SelectedCompetition.Type = Type;
            SelectedCompetition.Name = Name;
            SelectedCompetition.Place = Place;
            SelectedCompetition.StartDate = StartDate;
            SelectedCompetition.EndDate = EndDate;
            SelectedCompetition.System = System;
            SelectedCompetition.Cadence = Cadence;
            SelectedCompetition.Rule = Rule;
            SelectedCompetition.Category = Category;
            SelectedCompetition.Capacity = Capacity;

            await _competitionService.ModifierCompetitionAsync(SelectedCompetition);

            LoadCompetitions();
        }
        
    }
}
