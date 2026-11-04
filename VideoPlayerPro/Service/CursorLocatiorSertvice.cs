namespace VideoPlayerPro.Service;

public sealed class CursorLocatorService : IDisposable
{
    private readonly UIElement _root;
    private readonly DispatcherTimer _timer;

    private bool _isIdle;
    private bool _isListening;

    public event Action? OnCursorMoved;
    public event Action? OnCursorIdle;

    public CursorLocatorService(UIElement root, TimeSpan idleTimeout, bool isListening = false)
    {
        _root = root;

        _timer = new DispatcherTimer
        {
            Interval = idleTimeout
        };

        _timer.Tick += OnTimerTick;
        _root.PreviewMouseMove += OnMouseMove;
        _isListening = isListening;
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        if (_isListening == false)
            return;

        _timer.Stop();
        _timer.Start();

        if (_isIdle)
        {
            _isIdle = false;
            OnCursorMoved?.Invoke();
        }
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        _timer.Stop();

        if (!_isIdle)
        {
            _isIdle = true;
            OnCursorIdle?.Invoke();
        }
    }

    public void Dispose()
    {
        _timer.Stop();
        _timer.Tick -= OnTimerTick;
        _root.PreviewMouseMove -= OnMouseMove;
    }
}
