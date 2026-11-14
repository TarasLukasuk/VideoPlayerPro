namespace VideoPlayerPro.Patterns.State;

internal class StateContext
{
    public WindowControlesStateAbstract CurrentState { get; private set; }
    public WindowStateAbstract CurrentStete { get; private set; }

    public void SetState(WindowControlesStateAbstract newState) => CurrentState = newState;
    public void SetState(WindowStateAbstract newState) => CurrentStete = newState;
    public void EnterState(Window window) => CurrentStete.EnterState(window);
    public void ExitState(Window window) => CurrentStete.ExitState(window);
    public void ShowControles(MainWindow window) => CurrentState.ShowControles(window);
    public void HidenControles(MainWindow window) => CurrentState.HidenControles(window);
}