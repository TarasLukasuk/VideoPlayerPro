namespace VideoPlayerPro.Views;

public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel = new();
    private CursorLocatorService? _cursorService;
    private bool isMaximized;
    private bool isFullScreen;
    private bool _isBlock;

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
        if (_isBlock)
            return;

        if (isMaximized && isFullScreen)
        {
            TopBar.Visibility = Visibility.Collapsed;
            BottomBorder.Visibility = Visibility.Visible;
            return;
        }

        if (isMaximized && !isFullScreen)
        {
            if (isMaximized)
            {
                TopBar.Visibility = Visibility.Visible;
                BottomBorder.Visibility = Visibility.Visible;
                return;
            }

            TopBar.Visibility = Visibility.Visible;
            BottomBorder.Visibility = Visibility.Collapsed;
            return;
        }

        if (isFullScreen)
        {
            TopBar.Visibility = Visibility.Collapsed;
            BottomBorder.Visibility = Visibility.Visible;
            return;
        }

        TopBar.Visibility = Visibility.Visible;
        BottomBorder.Visibility = Visibility.Visible;
    }

    private void HideControls()
    {
        TopBar.Visibility = Visibility.Collapsed;
        BottomBorder.Visibility = Visibility.Collapsed;
        
    }

    private void RestoreWindow() => WindowState = WindowState.Normal;
    private void MinimizeWindow_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
    private void MaximizeWindows() => WindowState = WindowState.Maximized;
    private void CloseWindow_Click(object sender, RoutedEventArgs e) => Close();

    private void MaximizeWindow_Checked(object sender, RoutedEventArgs e)
    {
        isMaximized = true;

        MaximizeWindows();
    }

    private void MaximizeWindow_Unchecked(object sender, RoutedEventArgs e)
    {
        isMaximized = false;

        RestoreWindow();
    }

    private void FullScreenCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        isFullScreen = true;

        MaximizeWindows();

        TopBar.Visibility = Visibility.Collapsed;
    }

    private void FullScreenCheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        isFullScreen = false;

        if (isMaximized)
        {
            TopBar.Visibility = Visibility.Visible;
            return;
        }

        TopBar.Visibility = Visibility.Visible;

        RestoreWindow();
    }

    private void Block_Checked(object sender, RoutedEventArgs e)
    {
        _isBlock = true;

        TopBar.Visibility = Visibility.Collapsed;
        Menu.Visibility = Visibility.Collapsed;
        Cast.Visibility = Visibility.Collapsed;
        BottomBorder.Visibility = Visibility.Collapsed;

    }

    private void Block_Unchecked(object sender, RoutedEventArgs e)
    {
        _isBlock = false;

        TopBar.Visibility = Visibility.Visible;
        Menu.Visibility = Visibility.Visible;
        Cast.Visibility = Visibility.Visible;
        BottomBorder.Visibility = Visibility.Visible;
    }
}