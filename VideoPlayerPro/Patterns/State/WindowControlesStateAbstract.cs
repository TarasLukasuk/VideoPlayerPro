namespace VideoPlayerPro.Patterns.State;

internal abstract class WindowControlesStateAbstract
{
    public abstract void ShowControles(MainWindow window);
    public abstract void HidenControles(MainWindow window);

    public abstract bool IsHiddenControles { get; protected set; }
}
