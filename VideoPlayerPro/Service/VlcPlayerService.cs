namespace VideoPlayerPro.Service;

public sealed class VlcPlayerService : IDisposable
{
    private readonly LibVLC _libVlc;

    public MediaPlayer MediaPlayer { get; }

    public VlcPlayerService()
    {
        Core.Initialize();

        _libVlc = new LibVLC();
        MediaPlayer = new MediaPlayer(_libVlc);
    }

    public void OpenFile(string filePath)
    {
        using var media = new Media(_libVlc, filePath, FromType.FromPath);
        MediaPlayer.Play(media);
    }

    public void Dispose()
    {
        MediaPlayer.Dispose();
        _libVlc.Dispose();
    }
}