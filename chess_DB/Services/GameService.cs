using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using chess_DB.Models;

namespace chess_DB.Services;

public class GameService
{
    private readonly PlayerService _playerService;
    private readonly string _cheminFichier;
    private readonly JsonSerializerOptions _jsonOptions;

    public GameService()
    {
        _playerService = new PlayerService();
        
        // Dossier Data à la racine du projet
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string? projectDir = Directory.GetParent(baseDir)?.Parent?.Parent?.Parent?.FullName;

        _cheminFichier = Path.Combine(projectDir!, "Data", "games.json");

        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        // Créer le dossier si nécessaire
        string? dossier = Path.GetDirectoryName(_cheminFichier);
        if (dossier != null && !Directory.Exists(dossier))
            Directory.CreateDirectory(dossier);

        // Créer le fichier s'il n'existe pas
        if (!File.Exists(_cheminFichier))
            File.WriteAllText(_cheminFichier, "[]");
    }

    // --------------------------------------------------
    // 🔵 Lire toutes les parties
    // --------------------------------------------------
    public async Task<List<Game>> GetAllAsync()
    {
        try
        {
            string json = await File.ReadAllTextAsync(_cheminFichier);
            return JsonSerializer.Deserialize<List<Game>>(json, _jsonOptions) ?? new List<Game>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lecture parties : {ex.Message}");
            return new List<Game>();
        }
    }

    // --------------------------------------------------
    // 🟢 Ajouter une partie
    // --------------------------------------------------
    public async Task<bool> AddAsync(Game game)
    {
        try
        {
            var games = await GetAllAsync();
            var players = await _playerService.ObtenirTousLesJoueursAsync();
            var white = players.FirstOrDefault(p => p.Id == game.WhitePlayerId);
            var black = players.FirstOrDefault(p => p.Id == game.BlackPlayerId);
            
            if (white == null || black == null)
                return false;

            // 🔹 Mise à jour Elo
            UpdateElo(white, black, game.Result);

            // 🔹 Sauvegarde joueurs
            await _playerService.SaveAllAsync(players);
            
            games.Add(game);

            string json = JsonSerializer.Serialize(games, _jsonOptions);
            await File.WriteAllTextAsync(_cheminFichier, json);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur ajout partie : {ex.Message}");
            return false;
        }
    }

    // --------------------------------------------------
    // 🔍 Trouver une partie par Id
    // --------------------------------------------------
    public async Task<Game?> GetByIdAsync(Guid id)
    {
        var games = await GetAllAsync();
        return games.Find(g => g.Id == id);
    }

    // --------------------------------------------------
    // 📝 Modifier une partie
    // --------------------------------------------------
    public async Task<bool> UpdateAsync(Game game)
    {
        try
        {
            var games = await GetAllAsync();
            int index = games.FindIndex(g => g.Id == game.Id);

            if (index == -1)
                return false;

            games[index] = game;

            string json = JsonSerializer.Serialize(games, _jsonOptions);
            await File.WriteAllTextAsync(_cheminFichier, json);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur modification partie : {ex.Message}");
            return false;
        }
    }

    // --------------------------------------------------
    // ❌ Supprimer une partie
    // --------------------------------------------------
    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var games = await GetAllAsync();
            bool removed = games.RemoveAll(g => g.Id == id) > 0;

            if (!removed)
                return false;

            string json = JsonSerializer.Serialize(games, _jsonOptions);
            await File.WriteAllTextAsync(_cheminFichier, json);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur suppression partie : {ex.Message}");
            return false;
        }
    }
    
    private void UpdateElo(Player white, Player black, string result)
    {
        const int k = 20;

        double expectedWhite = 1 / (1 + Math.Pow(10, (black.Elo - white.Elo) / 400.0));
        double expectedBlack = 1 / (1 + Math.Pow(10, (white.Elo - black.Elo) / 400.0));

        double scoreWhite = 0;
        double scoreBlack = 0;

        switch (result)
        {
            case "1-0":
                scoreWhite = 1;
                scoreBlack = 0;
                break;
            case "0-1":
                scoreWhite = 0;
                scoreBlack = 1;
                break;
            case "1/2-1/2":
                scoreWhite = 0.5;
                scoreBlack = 0.5;
                break;
        }

        white.Elo = (int)(white.Elo + k * (scoreWhite - expectedWhite));
        black.Elo = (int)(black.Elo + k * (scoreBlack - expectedBlack));
    }

}
