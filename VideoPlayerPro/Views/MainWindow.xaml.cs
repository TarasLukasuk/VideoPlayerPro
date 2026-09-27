using System.Windows;

namespace VideoPlayerPro.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"{ex.ToString()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}