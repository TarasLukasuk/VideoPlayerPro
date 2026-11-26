namespace VideoPlayerPro.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly double minVolume = 0;
    private readonly double maxVolume = 200;
    private readonly VlcPlayerService _vlcPlayerService;
    private double _volume = 75;
    private double _previousVolume = 75;
    private bool _isMuted;

    public MainViewModel()
    {
        _vlcPlayerService = new VlcPlayerService();

        FileCommands = new FileCommands(_vlcPlayerService);
    }

    public IconsProviderService IconsProviderService { get; set; } = new IconsProviderService();
    public MediaPlayer MediaPlayer => _vlcPlayerService.MediaPlayer;
    public FileCommands FileCommands { get; }

    public double Volume
    {
        get => _volume;
        set
        {
            value = Math.Clamp(value, minVolume, maxVolume);

            if (!SetProperty(ref _volume, value))
                return;

            if (value == minVolume)
            {
                IsMuted = true;
            }
            else
            {
                _previousVolume = value;
                IsMuted = false;
            }
        }
    }

    public bool IsMuted
    {
        get => _isMuted;
        set
        {
            if (!SetProperty(ref _isMuted, value))
                return;

            if (value)
            {
                if (Volume > minVolume)
                    _previousVolume = Volume;

                Volume = minVolume;
            }
            else if (Volume == minVolume)
            {
                Volume = _previousVolume;
            }
        }
    }
}
