namespace VideoPlayerPro.ViewModels;

public class AudioViewModel : ViewModelBase
{
    public const double MinVolume = 0;
    public const double MaxVolume = 200;

    private double _volume = 75;
    private double _previousVolume = 75;

    public double Volume
    {
        get
        {
            return _volume;
        }

        set
        {
            value = Math.Clamp(value, MinVolume, MaxVolume);

            if (!SetProperty(ref _volume, value))
                return;

            if (_volume > MinVolume)
                _previousVolume = _volume;

            OnPropertyChanged(nameof(IsMuted));
        }
    }

    public bool IsMuted
    {
        get
        {
            return Volume == MinVolume;
        }

        set
        {
            if (value == IsMuted)
                return;

            if (value)
            {
                Volume = MinVolume;
            }
            else
            {
                Volume = _previousVolume;
            }
        }
    }


}