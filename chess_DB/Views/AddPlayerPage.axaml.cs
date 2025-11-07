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

public partial class AddPlayerPage : UserControl
{
    private const string FileForm = "player.json";
    
    public AddPlayerPage()
    {
        InitializeComponent();
    }
    
    private void GoToHomePage(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (VisualRoot is MainView mainView)
        {
            mainView.MainContent.Content = new HomePageView();
        }
    }

    private void SaveClick(object? sender, RoutedEventArgs e)
    {
        List<FormData> form = new();
        if (File.Exists(FileForm))
        {
            string oldJson=File.ReadAllText(FileForm);
            if (!string.IsNullOrWhiteSpace(oldJson))
            {
                form= JsonSerializer.Deserialize<List<FormData>>(oldJson);
            }
        }
        
        string gender=(GenderComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "";
        
        
        FormData newForm = new FormData
        {
            Name = NameTextBox.Text ?? "",
            Surname = SurnameTextBox.Text??"",
            Gender = gender,
            Birthdate = BirthdatePicker.SelectedDate?.DateTime,
            Email = EmailTextBox.Text??"",
            Phone = PhoneTextBox.Text??"",
            Country = CountryTextBox.Text??"",
            City = CityTextBox.Text??"",
            Street = StreetTextBox.Text??"",
            PostalCode = PostalCodeTextBox.Text??"",
        };
        
        form.Add(newForm);
        string json = JsonSerializer.Serialize(form, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FileForm, json);
        Console.WriteLine("Successfully saved");
        
        if (VisualRoot is MainView mainView)
        {
            mainView.MainContent.Content = new ConsultPlayerPage();
        }
    }
    
}