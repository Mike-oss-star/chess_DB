using Avalonia.Controls;
using chess_DB.Views;

namespace chess_DB;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
        MainContent.Content = new HomePageView();
    }
    
    private void ShowPlayerPage(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainContent.Content = new PlayerPageView();
    }
}
