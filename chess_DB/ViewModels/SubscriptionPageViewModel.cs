using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using chess_DB.Models;
using chess_DB.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace chess_DB.ViewModels;

public partial class SubscriptionPageViewModel : ViewModelBase
{
    private readonly CompetitionService _competitionService = new();
    private readonly PlayerService _playerService = new();

    public ObservableCollection<Competition> Competitions { get; } = new();
    public ObservableCollection<PlayerSelectable> Players { get; } = new();

    [ObservableProperty]
    private Competition? selectedCompetition;

    public SubscriptionPageViewModel()
    {
        LoadAsync();
    }

    private async Task LoadAsync()
    {
        var competitions = await _competitionService.ObtenirToutesLesCompetitionsAsync();
        Competitions.Clear();
        competitions.ForEach(c => Competitions.Add(c));

        var players = await _playerService.ObtenirTousLesJoueursAsync();
        Players.Clear();
        players.ForEach(p => Players.Add(new PlayerSelectable(p)));
    }

    partial void OnSelectedCompetitionChanged(Competition? value)
    {
        if (value == null) return;

        foreach (var p in Players)
            p.IsSelected = value.JoueursIds.Contains(p.Player.Id);
    }

    [RelayCommand]
    private async Task SaveSubscriptionAsync()
    {
        if (SelectedCompetition == null) return;

        var selectedPlayers = Players
            .Where(p => p.IsSelected)
            .Select(p => p.Player.Id)
            .ToList();

        if (selectedPlayers.Count > SelectedCompetition.Capacity)
            return;

        SelectedCompetition.JoueursIds = selectedPlayers;

        await _competitionService.ModifierCompetitionAsync(SelectedCompetition);
    }
}