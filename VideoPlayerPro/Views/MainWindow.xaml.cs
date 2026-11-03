namespace VideoPlayerPro.Views;

public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel = new();

    public MainWindow()
    {
        InitializeComponent();

        DataContext = _viewModel;
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