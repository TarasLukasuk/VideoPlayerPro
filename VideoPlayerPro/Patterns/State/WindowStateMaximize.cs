namespace VideoPlayerPro.Patterns.State;

internal class WindowStateMaximize : WindowStateAbstract
{
    public override bool IsEnter { get; protected set; }

    public override void EnterState(Window window)
    {
        IsEnter = true;

        if (window is MainWindow mainWindow && mainWindow.WindowStateFullScreen.IsHiddenControles)
        {
            return;
        }

        window.WindowState = WindowState.Maximized;
    }

    public override void ExitState(Window window)
    {
        IsEnter = false;

        if (window is MainWindow mainWindow && mainWindow.WindowStateFullScreen.IsHiddenControles)
        {
            return;
        }

        window.WindowState = WindowState.Normal;
    }
}