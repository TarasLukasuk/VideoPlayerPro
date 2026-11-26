namespace VideoPlayerPro.Service;

public sealed class CursorLocatorService : IDisposable
{
    private readonly UIElement _root;
    private readonly DispatcherTimer _timer;
    private readonly TimeSpan _idleTimeout;
    private readonly Stopwatch _idleStopwatch = new();

    private POINT? _lastPosition;

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
        _idleTimeout = idleTimeout;

        _timer = new DispatcherTimer(DispatcherPriority.Background, root.Dispatcher)
        {
            Interval = TimeSpan.FromMilliseconds(50)
        };

        _timer.Tick += OnTimerTick;
    }

    public void StartListening()
    {
        if (_isDisposed || _isListening)
            return;

        _isListening = true;
        _isIdle = false;
        _lastPosition = null;

        _idleStopwatch.Restart();
        _timer.Start();
    }

    public void StopListening()
    {
        if (!_isListening)
            return;

        _isListening = false;
        _isIdle = false;
        _lastPosition = null;

        _timer.Stop();
        _idleStopwatch.Reset();
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        if (!_isListening || _isDisposed)
            return;

        if (!GetCursorPos(out POINT position))
            return;

        bool hasMoved = !_lastPosition.HasValue || _lastPosition.Value.X != position.X || _lastPosition.Value.Y != position.Y;

        _lastPosition = position;

        if (hasMoved && IsCursorInsideRoot(position))
        {
            _idleStopwatch.Restart();

            if (_isIdle)
            {
                _isIdle = false;

                OnCursorMoved?.Invoke();
            }

            return;
        }

        if (!_isIdle && _idleStopwatch.Elapsed >= _idleTimeout)
        {
            _isIdle = true;

            OnCursorIdle?.Invoke();
        }
    }

    private bool IsCursorInsideRoot(POINT position)
    {
        if (!_root.IsVisible || PresentationSource.FromVisual(_root) is null)
        {
            return false;
        }

        Point localPosition = _root.PointFromScreen( new Point(position.X, position.Y));

        return new Rect(_root.RenderSize).Contains(localPosition);
    }

    public void Dispose()
    {
        if (_isDisposed)
            return;

        StopListening();

        _isDisposed = true;

        _timer.Tick -= OnTimerTick;

        OnCursorMoved = null;
        OnCursorIdle = null;
    }

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out POINT point);

    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int X;
        public int Y;
    }
}