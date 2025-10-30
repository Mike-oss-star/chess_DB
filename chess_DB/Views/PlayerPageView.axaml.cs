using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace chess_DB.Views;

public partial class PlayerPageView : UserControl
{
    public PlayerPageView()
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
}