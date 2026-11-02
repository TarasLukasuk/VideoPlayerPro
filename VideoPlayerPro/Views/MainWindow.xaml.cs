using System.Windows;
using VideoPlayerPro.ViewModels;

namespace VideoPlayerPro.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = _viewModel;
    }

    private void VolumeCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        if (VolumeSlider.Value > 0)
        {
            _previousVolume = VolumeSlider.Value;
        }

        VolumeSlider.Value = 0;
    }

    private void VolumeCheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        VolumeSlider.Value = _previousVolume;
    }

    private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (sender is not Slider slider)
            return;

        if (slider.Value <= 0)
        {
            if (e.OldValue > 0)
            {
                _previousVolume = e.OldValue;
            }

            VolumeCheckBox.IsChecked = true;
        }
        else
        {
            VolumeCheckBox.IsChecked = false;
        }
    }
}