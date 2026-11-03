namespace VideoPlayerPro.ViewModels;

public class MainViewModel : ViewModelBase
{
    private double minVolume = 0;
    private double maxVolume = 200;
    private double _volume = 75;
    private  double _previousVolume = 75;
    private bool _isMuted;

    public IconsProviderService IconsProviderService { get; set; } = new IconsProviderService();

    public double Volume
    {
        get => _volume;
        set
        {
            value = Math.Clamp(value, minVolume, maxVolume);

            if (!SetProperty(ref _volume, value))
                return;

            if (value > minVolume)
            {
                _previousVolume = value;
                IsMuted = false;
            }
            else
            {
                IsMuted = true;
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
