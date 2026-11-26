namespace VideoPlayerPro.ViewModels;

public sealed class FileCommands
{
    public ICommand OpenFile { get; }

    public FileCommands(VlcPlayerService vlcPlayerService)
    {
        OpenFile = new OpenFileCommand(vlcPlayerService);
    }
}