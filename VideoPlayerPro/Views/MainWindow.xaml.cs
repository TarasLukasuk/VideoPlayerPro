using System.Windows.Controls;
using System.Windows.Input;

namespace VideoPlayerPro.Views;

public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel = new();
    private CursorLocatorService? _cursorService;

    public MainWindow()
    {
        InitializeComponent();

        DataContext = _viewModel;

        Loaded += MainWindow_Loaded;
        Closed += MainWindow_Closed;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        _cursorService = new CursorLocatorService(MainGrid, TimeSpan.FromSeconds(3), true);

        _cursorService.OnCursorMoved += ShowControls;
        _cursorService.OnCursorIdle += HideControls;
    }

    private void MainWindow_Closed(object? sender, EventArgs e)
    {
        _cursorService?.Dispose();
    }

    private void ShowControls()
    {
        BottomBorder.Visibility = Visibility.Visible;
        TopBar.Visibility = Visibility.Visible;
    }

    private void HideControls()
    {
        BottomBorder.Visibility = Visibility.Collapsed;
        TopBar.Visibility = Visibility.Collapsed;
    }

    private void FullScreenCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Maximized;
        TopBar.Visibility = Visibility.Collapsed;
    }

    private void FullScreenCheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Normal;
        TopBar.Visibility = Visibility.Visible;
    }
}