namespace VideoPlayerPro.Service;

public sealed class CursorLocatorService : IDisposable
{
    private readonly UIElement _root;
    private readonly DispatcherTimer _timer;

    private bool _isIdle;
    private bool _isListening;
    private bool _isDisposed;

    public event Action? OnCursorMoved;
    public event Action? OnCursorIdle;

    public CursorLocatorService(UIElement root, TimeSpan idleTimeout)
    {
        ArgumentNullException.ThrowIfNull(root);

        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(idleTimeout, TimeSpan.Zero);

        _root = root;

        _timer = new DispatcherTimer(DispatcherPriority.Background, root.Dispatcher)
        {
            Interval = idleTimeout
        };

        _timer.Tick += OnTimerTick;
        _root.PreviewMouseMove += OnMouseMove;
    }

    public void StartListening()
    {
        if (_isDisposed || _isListening)
            return;

        _isListening = true;
        _isIdle = false;

        RestartTimer();
    }

    public void StopListening()
    {
        if (!_isListening)
            return;

        _isListening = false;
        _isIdle = false;

        _timer.Stop();
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        if (!_isListening || _isDisposed)
            return;

        if (_isIdle)
        {
            _isIdle = false;
            OnCursorMoved?.Invoke();
        }

        RestartTimer();
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        _timer.Stop();

        if (!_isListening || _isIdle || _isDisposed)
            return;

        _isIdle = true;

        OnCursorIdle?.Invoke();
    }

    private void RestartTimer()
    {
        _timer.Stop();
        _timer.Start();
    }

    public void Dispose()
    {
        if (_isDisposed)
            return;

        StopListening();

        _isDisposed = true;

        _timer.Tick -= OnTimerTick;
        _root.PreviewMouseMove -= OnMouseMove;

        OnCursorMoved = null;
        OnCursorIdle = null;
    }
}
