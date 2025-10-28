using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace chess_DB.Views;

public partial class HomePageView : UserControl
{
    public HomePageView()
    {
        InitializeComponent();
    }
    
    private void GoToPlayerPage(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // Trouver la fenêtre parente
        if (VisualRoot is MainView mainView)
        {
            // Remplacer le contenu du ContentControl principal
            mainView.MainContent.Content = new PlayerPageView();
        }
    }
}