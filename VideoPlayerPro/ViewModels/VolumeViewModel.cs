namespace VideoPlayerPro.ViewModels;

public class VolumeViewModel : ViewModelBase
{
    private const double MIN_VOLUME = 0;
    private const double MAX_VOLUME = 200;

    private double _volume = 75;
    private double _previousVolume = 75;
    private bool _isMuted;

    public double Volume
    {
        get => _volume;
        set
        {
            value = Math.Clamp(value, MIN_VOLUME, MAX_VOLUME);

            if (!SetProperty(ref _volume, value))
                return;

            if (value > MIN_VOLUME)
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
                if (Volume > MIN_VOLUME)
                    _previousVolume = Volume;

                Volume = MIN_VOLUME;
            }
            else if (Volume == MIN_VOLUME)
            {
                Volume = _previousVolume;
            }
        }
    }
}