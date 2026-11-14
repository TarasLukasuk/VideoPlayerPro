namespace VideoPlayerPro.Patterns.State;

internal class WindowStateMinimaze : WindowStateAbstract
{
    public override bool IsEnter { get; protected set; }

    public override void EnterState(Window window)
    {
        window.WindowState = WindowState.Minimized;
    }

    public override void ExitState(Window window)
    {
        throw new NotImplementedException();
    }
}
