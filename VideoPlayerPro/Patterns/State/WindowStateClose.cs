namespace VideoPlayerPro.Patterns.State;

internal class WindowStateClose : WindowStateAbstract
{
    public override bool IsEnter { get; protected set; }

    public override void EnterState(Window window)
    {
        window.Close();
    }

    public override void ExitState(Window window)
    {
        throw new NotImplementedException();
    }
}
