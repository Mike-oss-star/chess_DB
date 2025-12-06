using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace chess_DB.Views;

public partial class AddCompetitionPage : UserControl
{
    public AddCompetitionPage()
    {
        InitializeComponent();
    }
    
    private void GoToHomePage(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (VisualRoot is MainView mainView)
        {
            //mainView.MainContent.Content = new HomePageView();
        }
    }
}