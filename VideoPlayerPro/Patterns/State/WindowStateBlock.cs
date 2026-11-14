namespace VideoPlayerPro.Patterns.State;

internal class WindowStateBlock : WindowControlesStateAbstract
{
    public override bool IsHiddenControles { get; protected set; }

    public override void HidenControles(MainWindow window)
    {
        window.TopBar.Visibility = Visibility.Collapsed;
        window.Menu.Visibility = Visibility.Collapsed;
        window.Cast.Visibility = Visibility.Collapsed;
        window.BottomBorder.Visibility = Visibility.Collapsed;
        IsHiddenControles = true;
    }

    public override void ShowControles(MainWindow window)
    {
        if (IsHiddenControles)
        {
            window.TopBar.Visibility = Visibility.Collapsed;
        }

        window.Menu.Visibility = Visibility.Visible;
        window.Cast.Visibility = Visibility.Visible;
        window.BottomBorder.Visibility = Visibility.Visible;
        IsHiddenControles = false;
    }
}
