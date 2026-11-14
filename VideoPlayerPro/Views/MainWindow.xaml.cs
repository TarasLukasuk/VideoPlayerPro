namespace VideoPlayerPro.Views;

public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel = new();
    private CursorLocatorService? _cursorService;
    private readonly StateContext _stateContext = new();

    private readonly WindowStateBlock _windowStateBlock;
    private readonly WindowStateMaximize _windowStateMaximize;
    private readonly WindowStateFullScreen _windowStateFullScreen;
    private readonly WindowStateMinimaze _windowStateMinimaze;
    private readonly WindowStateClose _windowStateClose;

    public MainWindow()
    {
        InitializeComponent();

        DataContext = _viewModel;

        Loaded += MainWindow_Loaded;
        Closed += MainWindow_Closed;
        Activated += MainWindow_Activated;
        Deactivated += MainWindow_Deactivated;

        _windowStateBlock = new WindowStateBlock();
        _windowStateMaximize = new WindowStateMaximize();
        _windowStateFullScreen = new WindowStateFullScreen();
        _windowStateMinimaze = new WindowStateMinimaze();
        _windowStateClose = new WindowStateClose();
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

    private void HideControls()
    {
        if (FullScreenCheckBox.IsChecked == true)
        {
            TopBar.Visibility = Visibility.Collapsed;
        }

        BottomBorder.Visibility = Visibility.Collapsed;
    }

    private void ShowControls()
    {
        //if (_isBlock)
        //    return;


        //if (_isHidenToppBar)
        //{
        //    BottomBorder.Visibility = Visibility.Visible;
        //    return;
        //}

        //if (FullScreenCheckBox.IsChecked == false)
        //{
        //    TopBar.Visibility = Visibility.Visible;
        //}

        //if (_isNowFullScreen && !_isHidenToppBar)
        //{
        //    BottomBorder.Visibility = Visibility.Visible;
        //    return;
        //}

        //if (_isNowFullScreen)
        //{
        //    BottomBorder.Visibility = Visibility.Visible;
        //    return;
        //}
    }

    private void MainWindow_Activated(object? sender, EventArgs e)
    {
        _cursorService?.StartListening();
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

    private void CloseWindow_Click(object sender, RoutedEventArgs e)
    {
        _stateContext.SetState(_windowStateClose);
        _stateContext.EnterState(this);
    }

    private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
    {
        _stateContext.SetState(_windowStateMinimaze);
        _stateContext.EnterState(this);
    }

    private void FullScreenCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        _stateContext.SetState(_windowStateFullScreen);
        _stateContext.ShowControles(this);
    }

    private void FullScreenCheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        _stateContext.SetState(_windowStateFullScreen);
        _stateContext.HidenControles(this);
    }

    private void MaximizeWindow_Checked(object sender, RoutedEventArgs e)
    {
        _stateContext.SetState(_windowStateMaximize);
        _stateContext.EnterState(this);
    }

    private void MaximizeWindow_Unchecked(object sender, RoutedEventArgs e)
    {
        _stateContext.SetState(_windowStateMaximize);
        _stateContext.ExitState(this);
    }

    private void Block_Checked(object sender, RoutedEventArgs e)
    {
        _stateContext.SetState(_windowStateBlock);
        _stateContext.HidenControles(this);
    }

    private void Block_Unchecked(object sender, RoutedEventArgs e)
    {
        _stateContext.SetState(_windowStateBlock);
        _stateContext.ShowControles(this);
    }
}