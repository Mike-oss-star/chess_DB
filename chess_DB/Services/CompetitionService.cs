using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using chess_DB.Models;

namespace chess_DB.Services;

public class CompetitionService
{
    private readonly string _cheminFichier;
    private readonly JsonSerializerOptions _jsonOptions;

    public CompetitionService()
    {
        // Détermine le dossier Data à la racine du projet
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string? projectDir = Directory.GetParent(baseDir)?.Parent?.Parent?.Parent?.FullName;

        _cheminFichier = Path.Combine(projectDir, "Data", "competition.json");

        // Options JSON (lisible + insensible à la casse)
        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        // Création du dossier si manquant
        string? dossier = Path.GetDirectoryName(_cheminFichier);
        if (dossier != null && !Directory.Exists(dossier))
            Directory.CreateDirectory(dossier);

        // Création du fichier s'il n'existe pas
        if (!File.Exists(_cheminFichier))
            File.WriteAllText(_cheminFichier, "[]");
    }

    // --------------------------------------------------------
    // 🔵 Lire toutes les compétitions
    // --------------------------------------------------------
    public async Task<List<Competition>> ObtenirToutesLesCompetitionsAsync()
    {
        try
        {
            string json = await File.ReadAllTextAsync(_cheminFichier);

            var competitions = JsonSerializer.Deserialize<List<Competition>>(json, _jsonOptions);

            return competitions ?? new List<Competition>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la lecture des compétitions : {ex.Message}");
            return new List<Competition>();
        }
    }

    // --------------------------------------------------------
    // 🟢 Ajouter une compétition
    // --------------------------------------------------------
    public async Task<bool> AjouterCompetitionAsync(Competition competition)
    {
        try
        {
            var competitions = await ObtenirToutesLesCompetitionsAsync();

            competitions.Add(competition);

            string json = JsonSerializer.Serialize(competitions, _jsonOptions);

            await File.WriteAllTextAsync(_cheminFichier, json);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de l'ajout de la compétition : {ex.Message}");
            return false;
        }
    }

    // --------------------------------------------------------
    // 🔍 Trouver par Id
    // --------------------------------------------------------
    public async Task<Competition?> ObtenirCompetitionParIdAsync(Guid id)
    {
        var competitions = await ObtenirToutesLesCompetitionsAsync();

        return competitions.Find(c => c.Id == id);
    }

    // --------------------------------------------------------
    // 📝 Modifier une compétition
    // --------------------------------------------------------
    public async Task<bool> ModifierCompetitionAsync(Competition competition)
    {
        try
        {
            var competitions = await ObtenirToutesLesCompetitionsAsync();

            int index = competitions.FindIndex(c => c.Id == competition.Id);

            if (index == -1)
                return false;

            competitions[index] = competition;

            string json = JsonSerializer.Serialize(competitions, _jsonOptions);
            await File.WriteAllTextAsync(_cheminFichier, json);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la modification : {ex.Message}");
            return false;
        }
    }

    // --------------------------------------------------------
    // ❌ Supprimer une compétition
    // --------------------------------------------------------
    public async Task<bool> SupprimerCompetitionAsync(Guid id)
    {
        try
        {
            var competitions = await ObtenirToutesLesCompetitionsAsync();

            bool supprime = competitions.RemoveAll(c => c.Id == id) > 0;

            if (!supprime)
                return false;

            string json = JsonSerializer.Serialize(competitions, _jsonOptions);
            await File.WriteAllTextAsync(_cheminFichier, json);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la suppression : {ex.Message}");
            return false;
        }
    }
}
