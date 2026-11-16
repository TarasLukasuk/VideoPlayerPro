namespace VideoPlayerPro.Patterns.State;

internal class WindowControlsVisibilityState : WindowControlesStateAbstract
{
    public override bool IsHiddenControles { get; protected set; }

    public override void HidenControles(MainWindow window)
    {
        window.TopBar.Visibility = Visibility.Collapsed;
        window.BottomBorder.Visibility = Visibility.Collapsed;

        IsHiddenControles = true;
    }

    public override void ShowControles(MainWindow window)
    {
        if (window.WindowStateBlock.IsHiddenControles)
        {
            return;
        }

        window.BottomBorder.Visibility = Visibility.Visible;

        if (window.WindowStateFullScreen.IsHiddenControles)
        {
            window.TopBar.Visibility = Visibility.Collapsed;
        }
        else
        {
            window.TopBar.Visibility = Visibility.Visible;
        }

        IsHiddenControles = false;
    }
}