namespace VideoPlayerPro.Patterns.State;

internal class WindowStateFullScreen : WindowControlesStateAbstract
{
    public override bool IsHiddenControles { get; protected set; }

    public override void HidenControles(MainWindow window)
    {
        window.TopBar.Visibility = Visibility.Visible;

        if (IsHiddenControles)
            return;

        window.WindowState = WindowState.Normal;
        IsHiddenControles = true;
    }

    public override void ShowControles(MainWindow window)
    {
        window.TopBar.Visibility = Visibility.Collapsed;
        window.WindowState = WindowState.Maximized;
        IsHiddenControles = false;
    }
}
