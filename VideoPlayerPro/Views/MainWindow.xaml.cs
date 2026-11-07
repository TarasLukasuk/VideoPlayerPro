
namespace VideoPlayerPro.Views;

public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel = new();
    private CursorLocatorService? _cursorService;

    private bool _onlyShowTopBar;

    public MainWindow()
    {
        InitializeComponent();

        DataContext = _viewModel;

        Loaded += MainWindow_Loaded;
        Closed += MainWindow_Closed;
        Activated += MainWindow_Activated;
        Deactivated += MainWindow_Deactivated;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        _cursorService = new CursorLocatorService(this, TimeSpan.FromSeconds(3));

        _cursorService.OnCursorMoved += ShowControls;
        _cursorService.OnCursorIdle += HideControls;

        if (IsActive)
        {
            MainWindow_Activated(this, EventArgs.Empty);
        }
    }

    private void MainWindow_Activated(object? sender, EventArgs e)
    {
        _cursorService?.StartListening();

        ShowControls();
    }

    private void MainWindow_Deactivated(object? sender, EventArgs e)
    {
        _cursorService?.StopListening();
    }

    private void MainWindow_Closed(object? sender, EventArgs e)
    {
        _cursorService?.Dispose();
        _cursorService = null;
    }

    private void ShowControls()
    {
        UpdateControlsVisibility(true);
    }

    private void HideControls()
    {
        UpdateControlsVisibility(false);
    }

    private void UpdateControlsVisibility(bool showControls)
    {
        bool isFullScreen = FullScreenCheckBox.IsChecked == true;
        bool isMaximizeWindow = MaximizeWindow.IsChecked == true;
        bool isBlockChecked = Block.IsChecked == true;

        if (showControls)
        {
            TopBar.Visibility = Visibility.Visible;
            BottomBorder.Visibility = Visibility.Visible;
            Menu.Visibility = Visibility.Visible;
            Cast.Visibility = Visibility.Visible;

            _onlyShowTopBar = false;

            return;
        }

        if (isBlockChecked)
        {
            TopBar.Visibility = Visibility.Collapsed;
            BottomBorder.Visibility = Visibility.Collapsed;
            Menu.Visibility = Visibility.Collapsed;
            Cast.Visibility = Visibility.Collapsed;

            return;
        }

        if (isFullScreen)
        {
            TopBar.Visibility = Visibility.Collapsed;
            BottomBorder.Visibility = Visibility.Collapsed;

            return;
        }

        if (isMaximizeWindow)
        {
            TopBar.Visibility = Visibility.Visible;
            BottomBorder.Visibility = Visibility.Collapsed;

            return;
        }

        TopBar.Visibility = Visibility.Collapsed;
        BottomBorder.Visibility = Visibility.Collapsed;
        Menu.Visibility = Visibility.Visible;
        Cast.Visibility = Visibility.Visible;
    }

    private void FullScreenCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        if (MaximizeWindow.IsChecked == true)
        {
            _onlyShowTopBar = true;

            return;
        }

        WindowState = WindowState.Maximized;

        ShowControls();
    }

    private void FullScreenCheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        if (MaximizeWindow.IsChecked == true)
        {
            TopBar.Visibility = Visibility.Visible;

            _onlyShowTopBar = false;

            return;
        }

        WindowState = WindowState.Normal;

        ShowControls();
    }

    private void Block_Checked(object sender, RoutedEventArgs e)
    {
        HideControls();
    }

    private void Block_Unchecked(object sender, RoutedEventArgs e)
    {
        ShowControls();
    }


    private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void MaximizeWindow_Checked(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Maximized;

        ShowControls();
    }

    private void MaximizeWindow_Unchecked(object sender, RoutedEventArgs e)
    {
        if (FullScreenCheckBox.IsChecked == true)
        {
            _onlyShowTopBar = false;

            return;
        }

        WindowState = WindowState.Normal;

        ShowControls();
    }

    private void CloseWindow_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}