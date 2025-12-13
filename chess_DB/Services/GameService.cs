using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using chess_DB.Models;

namespace chess_DB.Services;

public class GameService
{
    private readonly string _cheminFichier;
    private readonly JsonSerializerOptions _jsonOptions;

    public GameService()
    {
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
}
