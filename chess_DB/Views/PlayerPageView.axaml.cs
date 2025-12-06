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
            //mainView.MainContent.Content = new HomePageView();
        }
    }
    
    private void GoToAddPlayerPage(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (VisualRoot is MainView mainView)
        {
            //mainView.MainContent.Content = new AddPlayerPage();
        }
    }
    
    private void GoToConsultPlayerPage(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (VisualRoot is MainView mainView)
        {
            //mainView.MainContent.Content = new ConsultPlayerPage();
        }
    }
}