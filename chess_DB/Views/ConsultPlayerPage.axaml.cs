using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using chess_DB.Models;

namespace chess_DB.Views;

public partial class ConsultPlayerPage : UserControl
{
    private const string FileForm = "player.json";
    private List<FormData> form = new();
    
    public ConsultPlayerPage()
    {
        InitializeComponent();
        FormListBox.ItemsSource = form;
        LoadForm();
    }
    
    private void LoadForm()
    {
        if (File.Exists(FileForm))
        {
            try
            {
                string json = File.ReadAllText(FileForm);
                form = JsonSerializer.Deserialize<List<FormData>>(json) ?? new();
            }
            catch
            {
                form = new();
            }
        }

        FormListBox.ItemsSource = null;
        FormListBox.ItemsSource = form;
        
    }
    
    private void GoToAddPlayerPage(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (VisualRoot is MainView mainView)
        {
            mainView.MainContent.Content = new AddPlayerPage();
        }
    }
}