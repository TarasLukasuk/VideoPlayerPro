namespace VideoPlayerPro.Patterns.State
{
    internal abstract class WindowStateAbstract
    {
        public abstract void EnterState(Window window);
        public abstract void ExitState(Window window);

        public abstract bool IsEnter { get; protected set; }
    }
}