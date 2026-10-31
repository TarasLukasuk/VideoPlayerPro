using System.Windows;
using VideoPlayerPro.ViewModels;

namespace VideoPlayerPro.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainViewModel();
    }
}