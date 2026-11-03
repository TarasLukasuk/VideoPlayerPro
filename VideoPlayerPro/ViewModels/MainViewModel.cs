namespace VideoPlayerPro.ViewModels;

public class MainViewModel : ViewModelBase
{
    public IconsProviderService IconsProviderService { get; set; } = new IconsProviderService();
    public VolumeViewModel VolumeViewModel { get; } = new VolumeViewModel();
}
