namespace VideoPlayerPro.Patterns.State;

internal class WindowStateFullScreen : WindowControlesStateAbstract
{
    public override bool IsHiddenControles { get; protected set; }

    public override void HidenControles(MainWindow window)
    {
        IsHiddenControles = false;

        if (window.WindowStateMaximize.IsEnter)
        {
            window.WindowState = WindowState.Maximized;
        }
        else
        {
            window.WindowState = WindowState.Normal;
        }

        if (window.WindowStateBlock.IsHiddenControles)
        {
            window.TopBar.Visibility = Visibility.Collapsed;
        }
        else
        {
            window.TopBar.Visibility = Visibility.Visible;
        }
    }

    public override void ShowControles(MainWindow window)
    {
        IsHiddenControles = true;

        window.TopBar.Visibility = Visibility.Collapsed;
        window.WindowState = WindowState.Maximized;
    }
}