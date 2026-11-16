namespace VideoPlayerPro.Patterns.State;

internal class WindowStateBlock : WindowControlesStateAbstract
{
    public override bool IsHiddenControles { get; protected set; }

    public override void HidenControles(MainWindow window)
    {
        IsHiddenControles = true;

        window.TopBar.Visibility = Visibility.Collapsed;
        window.Menu.Visibility = Visibility.Collapsed;
        window.Cast.Visibility = Visibility.Collapsed;
        window.BottomBorder.Visibility = Visibility.Collapsed;
    }

    public override void ShowControles(MainWindow window)
    {
        IsHiddenControles = false;

        if (window.WindowStateFullScreen.IsHiddenControles)
        {
            window.TopBar.Visibility = Visibility.Collapsed;
        }
        else
        {
            window.TopBar.Visibility = Visibility.Visible;
        }

        window.Menu.Visibility = Visibility.Visible;
        window.Cast.Visibility = Visibility.Visible;
        window.BottomBorder.Visibility = Visibility.Visible;
    }
}