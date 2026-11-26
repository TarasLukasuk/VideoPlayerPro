namespace VideoPlayerPro.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly VlcPlayerService _vlcPlayerService;

    public MainViewModel()
    {
        _vlcPlayerService = new VlcPlayerService();

        Audio = new AudioViewModel();
        FileCommands = new FileCommands(_vlcPlayerService);
    }

    public AudioViewModel Audio { get; }
    public IconsProviderService IconsProviderService { get; } = new IconsProviderService();
    public MediaPlayer MediaPlayer => _vlcPlayerService.MediaPlayer;
    public FileCommands FileCommands { get; }
}