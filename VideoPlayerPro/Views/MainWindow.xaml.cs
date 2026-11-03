namespace VideoPlayerPro.Views;

public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel = new();

    public MainWindow()
    {
        InitializeComponent();

        DataContext = _viewModel;
    }
}