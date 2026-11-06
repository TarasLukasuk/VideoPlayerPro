namespace VideoPlayerPro.Controls;

public partial class TriangleSlider : UserControl
{
    private GradientStop? _gradientStartStop;
    private GradientStop? _gradientMiddleStop;
    private GradientStop? _gradientEndStop;

    public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(nameof(Minimum), typeof(double), typeof(TriangleSlider), new PropertyMetadata(0d));
    public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(TriangleSlider), new PropertyMetadata(100d));
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(double), typeof(TriangleSlider), new FrameworkPropertyMetadata( 0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    public static readonly DependencyProperty GradientStartColorProperty = DependencyProperty.Register(nameof(GradientStartColor), typeof(Color), typeof(TriangleSlider), new PropertyMetadata(Color.FromRgb(0x58, 0x30, 0xFD), OnGradientStartColorChanged));
    public static readonly DependencyProperty GradientStartOffsetProperty = DependencyProperty.Register(nameof(GradientStartOffset), typeof(double), typeof(TriangleSlider), new PropertyMetadata(0d, OnGradientStartOffsetChanged));
    public static readonly DependencyProperty GradientMiddleColorProperty = DependencyProperty.Register(nameof(GradientMiddleColor), typeof(Color), typeof(TriangleSlider), new PropertyMetadata(Color.FromRgb(0x7C, 0x5C, 0xFF), OnGradientMiddleColorChanged));
    public static readonly DependencyProperty GradientMiddleOffsetProperty = DependencyProperty.Register(nameof(GradientMiddleOffset), typeof(double), typeof(TriangleSlider), new PropertyMetadata(0.5d, OnGradientMiddleOffsetChanged));
    public static readonly DependencyProperty GradientEndColorProperty = DependencyProperty.Register(nameof(GradientEndColor), typeof(Color), typeof(TriangleSlider), new PropertyMetadata(Colors.White, OnGradientEndColorChanged));
    public static readonly DependencyProperty GradientEndOffsetProperty = DependencyProperty.Register( nameof(GradientEndOffset), typeof(double), typeof(TriangleSlider), new PropertyMetadata(1d, OnGradientEndOffsetChanged));

    public double Minimum
    {
        get => (double)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    public double Maximum
    {
        get => (double)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }


    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public Color GradientStartColor
    {
        get => (Color)GetValue(GradientStartColorProperty);
        set => SetValue(GradientStartColorProperty, value);
    }

    public double GradientStartOffset
    {
        get => (double)GetValue(GradientStartOffsetProperty);
        set => SetValue(GradientStartOffsetProperty, value);
    }

    public Color GradientMiddleColor
    {
        get => (Color)GetValue(GradientMiddleColorProperty);
        set => SetValue(GradientMiddleColorProperty, value);
    }

    public double GradientMiddleOffset
    {
        get => (double)GetValue(GradientMiddleOffsetProperty);
        set => SetValue(GradientMiddleOffsetProperty, value);
    }

    public Color GradientEndColor
    {
        get => (Color)GetValue(GradientEndColorProperty);
        set => SetValue(GradientEndColorProperty, value);
    }

    public double GradientEndOffset
    {
        get => (double)GetValue(GradientEndOffsetProperty);
        set => SetValue(GradientEndOffsetProperty, value);
    }

    public TriangleSlider()
    {
        InitializeComponent();

        InitializeGradient();
    }

    private void InitializeGradient()
    {
        _gradientStartStop = new GradientStop(GradientStartColor, GradientStartOffset);
        _gradientMiddleStop = new GradientStop(GradientMiddleColor, GradientMiddleOffset);
        _gradientEndStop = new GradientStop(GradientEndColor, GradientEndOffset);

        var gradientBrush = new LinearGradientBrush
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 0)
        };

        gradientBrush.GradientStops.Add(_gradientStartStop);
        gradientBrush.GradientStops.Add(_gradientMiddleStop);
        gradientBrush.GradientStops.Add(_gradientEndStop);

        GradientBackground.Fill = gradientBrush;
    }

    private static void OnGradientStartColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        NewMethod(d, e);
    }

    private static void OnGradientStartOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        NewMethod1(d, e);
    }

    private static void OnGradientEndColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        NewMethod(d, e);
    }

    private static void OnGradientEndOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        NewMethod1(d, e);
    }

    private static void OnGradientMiddleColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        NewMethod(d, e);
    }


    private static void OnGradientMiddleOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        NewMethod1(d, e);
    }

    private static void NewMethod(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = d as TriangleSlider;

        if (control._gradientMiddleStop != null)
            control._gradientMiddleStop.Color = (Color)e.NewValue;
    }


    private static void NewMethod1(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = d as TriangleSlider;

        if (control._gradientMiddleStop != null)
            control._gradientMiddleStop.Offset = (double)e.NewValue;
    }
}