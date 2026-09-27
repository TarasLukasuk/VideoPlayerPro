using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace VideoPlayerPro.Controls
{
    public partial class TimeLine : UserControl
    {
        private const int DEFAULT = 0;
        private const int MAX_NORMALIZED_VALUE = 1;
        private const int THUMB_CENTER_DIVISOR = 2;

        private bool _isDragging;
        private bool _isCursorOverTimeline;

        public TimeLine()
        {
            InitializeComponent();

            Loaded += TimeLine_Loaded;
        }

        private void TimeLine_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateVisuals();
        }

        public static readonly DependencyProperty TimeLineMinValueProperty = DependencyProperty.Register(nameof(TimeLineMinValue), typeof(double), typeof(TimeLine), new FrameworkPropertyMetadata(0.0,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTimelinePropertyChanged));
        public static readonly DependencyProperty TimeLineMaxValueProperty = DependencyProperty.Register(nameof(TimeLineMaxValue), typeof(double), typeof(TimeLine), new FrameworkPropertyMetadata(100.0,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTimelinePropertyChanged));
        public static readonly DependencyProperty TimeLineCurrentValueProperty = DependencyProperty.Register(nameof(TimeLineCurrentValue), typeof(double), typeof(TimeLine), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTimelinePropertyChanged));
        public static readonly DependencyProperty TimeLineStepProperty = DependencyProperty.Register(nameof(TimeLineStep), typeof(double), typeof(TimeLine), new FrameworkPropertyMetadata(0.0));
        public static readonly DependencyProperty BufferedValueProperty = DependencyProperty.Register(nameof(BufferedValue), typeof(double), typeof(TimeLine), new FrameworkPropertyMetadata(0.0, OnTimelinePropertyChanged));
        public static readonly DependencyProperty TimelineHeightProperty = DependencyProperty.Register(nameof(TimelineHeight), typeof(double), typeof(TimeLine), new FrameworkPropertyMetadata(8.0, OnTimelinePropertyChanged));
        public static readonly DependencyProperty ThumbSizeProperty = DependencyProperty.Register(nameof(ThumbSize), typeof(double), typeof(TimeLine), new FrameworkPropertyMetadata(16.0, OnTimelinePropertyChanged));
        public static readonly DependencyProperty TimelineMarginProperty = DependencyProperty.Register(nameof(TimelineMargin), typeof(Thickness), typeof(TimeLine), new FrameworkPropertyMetadata(new Thickness(8, 0, 8, 0), OnTimelinePropertyChanged));
        public static readonly DependencyProperty TimelineCornerRadiusProperty = DependencyProperty.Register(nameof(TimelineCornerRadius), typeof(CornerRadius), typeof(TimeLine), new FrameworkPropertyMetadata(new CornerRadius(2), OnTimelinePropertyChanged));
        public static readonly DependencyProperty ThumbColorProperty = DependencyProperty.Register(nameof(ThumbColor), typeof(Brush), typeof(TimeLine), new FrameworkPropertyMetadata(Brushes.White, OnTimelinePropertyChanged));
        public static readonly DependencyProperty TimelineBackgroundColorProperty = DependencyProperty.Register(nameof(TimelineBackgroundColor), typeof(Brush), typeof(TimeLine), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(48, 53, 62)), OnTimelinePropertyChanged));
        public static readonly DependencyProperty TimelineBufferBackgroundColorProperty = DependencyProperty.Register(nameof(TimelineBufferBackgroundColor), typeof(Brush), typeof(TimeLine), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(90, 96, 107)), OnTimelinePropertyChanged));
        public static readonly DependencyProperty TimelineProgressBackgroundColorProperty = DependencyProperty.Register(nameof(TimelineProgressBackgroundColor), typeof(Brush), typeof(TimeLine), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(124, 92, 255)), OnTimelinePropertyChanged));
        public static readonly DependencyProperty FollowToCursorColorProperty = DependencyProperty.Register(nameof(FollowToCursorColor), typeof(Brush), typeof(TimeLine), new FrameworkPropertyMetadata(Brushes.White, OnTimelinePropertyChanged));
        public static readonly DependencyProperty FollowToCursorOpacityProperty = DependencyProperty.Register(nameof(FollowToCursorOpacity), typeof(double), typeof(TimeLine), new FrameworkPropertyMetadata(0.5, OnTimelinePropertyChanged));

        [Category("Settings")]
        [Description("Minimum value of the timeline.")]
        public double TimeLineMinValue
        {
            get => (double)GetValue(TimeLineMinValueProperty);
            set => SetValue(TimeLineMinValueProperty, value);
        }

        [Category("Settings")]
        [Description("Maximum value of the timeline.")]
        public double TimeLineMaxValue
        {
            get => (double)GetValue(TimeLineMaxValueProperty);
            set => SetValue(TimeLineMaxValueProperty, value);
        }

        [Category("Settings")]
        [Description("Current value of the timeline.")]
        public double TimeLineCurrentValue
        {
            get => (double)GetValue(TimeLineCurrentValueProperty);
            set => SetValue(TimeLineCurrentValueProperty, value);
        }

        [Category("Settings")]
        [Description("Step value of the timeline.")]
        public double TimeLineStep
        {
            get => (double)GetValue(TimeLineStepProperty);
            set => SetValue(TimeLineStepProperty, value);
        }

        [Category("Settings")]
        [Description("Buffered value of the timeline.")]
        public double BufferedValue
        {
            get => (double)GetValue(BufferedValueProperty);
            set => SetValue(BufferedValueProperty, value);
        }

        [Category("Appearance")]
        [Description("Height of the timeline.")]
        public double TimelineHeight
        {
            get => (double)GetValue(TimelineHeightProperty);
            set => SetValue(TimelineHeightProperty, value);
        }

        [Category("Appearance")]
        [Description("Size of the timeline thumb.")]
        public double ThumbSize
        {
            get => (double)GetValue(ThumbSizeProperty);
            set => SetValue(ThumbSizeProperty, value);
        }

        [Category("Appearance")]
        [Description("Horizontal margin of the timeline track.")]
        public Thickness TimelineMargin
        {
            get => (Thickness)GetValue(TimelineMarginProperty);
            set => SetValue(TimelineMarginProperty, value);
        }

        [Category("Appearance")]
        [Description("Corner radius of the timeline.")]
        public CornerRadius TimelineCornerRadius
        {
            get => (CornerRadius)GetValue(TimelineCornerRadiusProperty);
            set => SetValue(TimelineCornerRadiusProperty, value);
        }

        [Category("Appearance")]
        public Brush ThumbColor
        {
            get => (Brush)GetValue(ThumbColorProperty);
            set => SetValue(ThumbColorProperty, value);
        }

        [Category("Appearance")]
        public Brush TimelineBackgroundColor
        {
            get => (Brush)GetValue(TimelineBackgroundColorProperty);
            set => SetValue(TimelineBackgroundColorProperty, value);
        }

        [Category("Appearance")]
        public Brush TimelineBufferBackgroundColor
        {
            get => (Brush)GetValue(TimelineBufferBackgroundColorProperty);
            set => SetValue(TimelineBufferBackgroundColorProperty, value);
        }

        [Category("Appearance")]
        public Brush TimelineProgressBackgroundColor
        {
            get => (Brush)GetValue(TimelineProgressBackgroundColorProperty);
            set => SetValue(TimelineProgressBackgroundColorProperty, value);
        }

        [Category("Appearance")]
        public Brush FollowToCursorColor
        {
            get => (Brush)GetValue(FollowToCursorColorProperty);
            set => SetValue(FollowToCursorColorProperty, value);
        }

        [Category("Appearance")]
        public double FollowToCursorOpacity
        {
            get => (double)GetValue(FollowToCursorOpacityProperty);
            set => SetValue(FollowToCursorOpacityProperty, value);
        }

        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double>), typeof(TimeLine));

        public event RoutedPropertyChangedEventHandler<double> ValueChanged
        {
            add => AddHandler(ValueChangedEvent, value);
            remove => RemoveHandler(ValueChangedEvent, value);
        }

        private static void OnTimelinePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not TimeLine timeline)
                return;

            if (e.Property == TimeLineCurrentValueProperty)
            {
                timeline.RaiseEvent(new RoutedPropertyChangedEventArgs<double>((double)e.OldValue, (double)e.NewValue, ValueChangedEvent));
            }

            timeline.UpdateVisuals();

            if (timeline._isCursorOverTimeline && !timeline._isDragging)
            {
                Point position = Mouse.GetPosition(timeline.TimelineRoot);

                timeline.UpdateFollowToCursor(position.X);
            }
        }

        private void TimelineRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(TimelineRoot);

            _isCursorOverTimeline = true;

            SetValueFromMousePosition(position.X);

            e.Handled = true;
        }

        private void TimelineRoot_MouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(TimelineRoot);

            _isCursorOverTimeline = true;

            if (!_isDragging)
            {
                UpdateFollowToCursor(position.X);
            }
        }

        private void TimelineRoot_MouseLeave(object sender, MouseEventArgs e)
        {
            _isCursorOverTimeline = false;

            if (!_isDragging)
            {
                FollowToCursor.Width = DEFAULT;
            }
        }

        private void TimelineThumb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isDragging = true;

            FollowToCursor.Width = 0;

            TimelineThumb.CaptureMouse();

            e.Handled = true;
        }

        private void TimelineThumb_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging)
                return;

            Point position = e.GetPosition(TimelineRoot);

            SetValueFromMousePosition(position.X);
        }

        private void TimelineThumb_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!_isDragging)
                return;

            _isDragging = false;

            TimelineThumb.ReleaseMouseCapture();

            Point position = e.GetPosition(TimelineRoot);

            if (_isCursorOverTimeline)
            {
                UpdateFollowToCursor(position.X);
            }

            e.Handled = true;
        }

        private void SetValueFromMousePosition(double mouseX)
        {
            double trackWidth = GetTrackWidth();

            if (trackWidth <= DEFAULT)
                return;

            double trackStart = TimelineMargin.Left;
            double relativeX = mouseX - trackStart;

            relativeX = Math.Max(DEFAULT, relativeX);
            relativeX = Math.Min(trackWidth, relativeX);

            double percentage = relativeX / trackWidth;
            double min = TimeLineMinValue;
            double max = TimeLineMaxValue;

            if (max <= min)
            {
                TimeLineCurrentValue = min;
                return;
            }

            double value = min + ((max - min) * percentage);

            if (TimeLineStep > DEFAULT)
            {
                value = Math.Round((value - min) / TimeLineStep) * TimeLineStep + min;
            }

            value = Math.Max(min, Math.Min(max, value));

            TimeLineCurrentValue = value;
        }

        private void UpdateFollowToCursor(double mouseX)
        {
            double trackWidth = GetTrackWidth();

            if (trackWidth <= DEFAULT)
            {
                FollowToCursor.Width = DEFAULT;
                return;
            }

            double min = TimeLineMinValue;
            double max = TimeLineMaxValue;

            if (max <= min)
            {
                FollowToCursor.Width = DEFAULT;
                return;
            }

            double trackStart = TimelineMargin.Left;
            double cursorX = mouseX - trackStart;

            cursorX = Math.Max(DEFAULT, Math.Min(trackWidth, cursorX));

            double current = Math.Max(min, Math.Min(max, TimeLineCurrentValue));
            double currentPercentage = (current - min) / (max - min);

            currentPercentage = Math.Max(DEFAULT, Math.Min(MAX_NORMALIZED_VALUE, currentPercentage));

            double currentX = trackWidth * currentPercentage;
            double startX =Math.Min(cursorX, currentX);
            double width = Math.Abs(cursorX - currentX);

            FollowToCursor.Margin = new Thickness(trackStart + startX, DEFAULT, DEFAULT, DEFAULT);

            FollowToCursor.Width = width;
        }

        private void UpdateVisuals()
        {
            if (!IsInitialized)
                return;

            double min = TimeLineMinValue;
            double max = TimeLineMaxValue;

            if (max <= min)
            {
                TimelineProgress.Width = DEFAULT;
                TimelineBuffer.Width = DEFAULT;
                FollowToCursor.Width = DEFAULT;
                return;
            }

            double current = Math.Max(min, Math.Min(max, TimeLineCurrentValue));
            double buffered = Math.Max(min, Math.Min(max, BufferedValue));
            double percentage = (current - min) / (max - min);
            double bufferPercentage = (buffered - min) / (max - min);

            percentage = Math.Max(DEFAULT, Math.Min(MAX_NORMALIZED_VALUE, percentage));
            bufferPercentage = Math.Max(DEFAULT, Math.Min(MAX_NORMALIZED_VALUE, bufferPercentage));

            double trackWidth = GetTrackWidth();

            if (trackWidth <= DEFAULT)
                return;

            TimelineBuffer.Width = trackWidth * bufferPercentage;
            TimelineProgress.Width = trackWidth * percentage;

            double trackStart = TimelineMargin.Left;
            double thumbX = trackStart + (trackWidth * percentage) - (ThumbSize / THUMB_CENTER_DIVISOR);

            TimelineThumb.Margin = new Thickness(thumbX, DEFAULT, DEFAULT, DEFAULT);

            if (_isCursorOverTimeline && !_isDragging)
            {
                Point mousePosition = Mouse.GetPosition(TimelineRoot);

                UpdateFollowToCursor(mousePosition.X);
            }
        }

        private double GetTrackWidth()
        {
            return Math.Max(DEFAULT, TimelineRoot.ActualWidth - TimelineMargin.Left - TimelineMargin.Right);
        }

        private void TimelineRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateVisuals();
        }
    }
}