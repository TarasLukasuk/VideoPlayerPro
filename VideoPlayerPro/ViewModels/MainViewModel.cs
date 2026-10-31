using VideoPlayerPro.Service;
using VideoPlayerPro.ViewModels.Base;

namespace VideoPlayerPro.ViewModels;

public class MainViewModel : ViewModelBase
{
	public IconsProviderService IconsProviderService { get; set; } = new IconsProviderService();
}
