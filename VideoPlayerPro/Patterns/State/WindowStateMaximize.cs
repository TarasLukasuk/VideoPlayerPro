namespace VideoPlayerPro.Patterns.State;

internal class WindowStateMaximize : WindowStateAbstract
{
    public override bool IsEnter { get; protected set; }

    public override void EnterState(Window window)
    {
        window.WindowState = WindowState.Maximized;
        IsEnter = true;
    }

    public override void ExitState(Window window)
    {
        window.WindowState = WindowState.Normal;
        IsEnter = false;
    }
}