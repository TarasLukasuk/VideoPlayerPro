namespace VideoPlayerPro.Commands;

internal sealed class OpenFileCommand : ICommand
{
    private const string VideoFileFilter =
        "MP4 (*.mp4)|*.mp4|" +
        "Matroska (*.mkv)|*.mkv|" +
        "AVI (*.avi)|*.avi|" +
        "QuickTime (*.mov)|*.mov|" +
        "Windows Media (*.wmv)|*.wmv|" +
        "WebM (*.webm)|*.webm|" +
        "Flash Video (*.flv)|*.flv|" +
        "M4V (*.m4v)|*.m4v|" +
        "MPEG (*.mpeg, *.mpg)|*.mpeg;*.mpg|" +
        "MPEG Transport Stream (*.ts, *.m2ts)|*.ts;*.m2ts|" +
        "3GP (*.3gp)|*.3gp|" +
        "Ogg Video (*.ogv)|*.ogv";

    private readonly VlcPlayerService _vlcPlayerService;

    public OpenFileCommand(VlcPlayerService vlcPlayerService)
    {
        _vlcPlayerService = vlcPlayerService;
    }

    private readonly OpenFileDialog _openFileDialog = new()
    {
        Filter = VideoFileFilter,
        FilterIndex = 1,
        Multiselect = false,
        CheckFileExists = true,
        CheckPathExists = true,
        ValidateNames = true,
        RestoreDirectory = true
    };

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        try
        {
            if (_openFileDialog.ShowDialog() != true)
            {
                return;
            }
        }
        catch (UnauthorizedAccessException exception)
        {
            HandleError("Access denied.", exception);
            return;
        }
        catch (Win32Exception exception)
        {
            HandleError("System dialog error.", exception);
            return;
        }
        catch (COMException exception)
        {
            HandleError("Windows interaction error.", exception);
            return;
        }

        _vlcPlayerService.OpenFile(_openFileDialog.FileName);
    }

    private static void HandleError(string message, Exception exception)
    {
        Trace.TraceError($"{message}{Environment.NewLine}{exception}");

        MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}