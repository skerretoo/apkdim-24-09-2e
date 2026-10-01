using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;

namespace drugieokno;

public partial class MainWindow : Window
{
    private const int Rows = 12;
    private const int Cols = 16;

    // Muted, low-contrast palettes — deliberately NOT eye-searing, no saturated red.
    private static readonly Color PaletteA1 = Color.Parse("#7FB3B0"); // muted teal
    private static readonly Color PaletteA2 = Color.Parse("#E8E0D0"); // warm light beige
    private static readonly Color PaletteB1 = Color.Parse("#8AB8D1"); // muted blue-gray
    private static readonly Color PaletteB2 = Color.Parse("#D9D5C9"); // light warm gray

    private readonly Border[,] _cells = new Border[Rows, Cols];
    private readonly DispatcherTimer _boardTimer;
    private readonly DispatcherTimer _motionTimer;
    private readonly DateTime _motionStart = DateTime.Now;

    private bool _showAlternate;
    private bool _animationEnabled = true;

    public MainWindow()
    {
        InitializeComponent();
        BuildBoard();
        ApplyPalette(false);

        // Slow toggle: 1.2 s interval ≈ 0.4 Hz, well below WCAG 3-flashes-per-second limit.
        _boardTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1.2) };
        _boardTimer.Tick += (_, _) =>
        {
            _showAlternate = !_showAlternate;
            ApplyPalette(_showAlternate);
        };
        _boardTimer.Start();

        // Gentle text drift (small sine bob, not a hard jump).
        _motionTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(50) };
        _motionTimer.Tick += (_, _) => UpdateTextPosition();
        _motionTimer.Start();
    }

    private void BuildBoard()
    {
        CheckerBoard.RowDefinitions.Clear();
        CheckerBoard.ColumnDefinitions.Clear();
        for (var r = 0; r < Rows; r++)
            CheckerBoard.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        for (var c = 0; c < Cols; c++)
            CheckerBoard.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        for (var r = 0; r < Rows; r++)
        {
            for (var c = 0; c < Cols; c++)
            {
                var cell = new Border();
                Grid.SetRow(cell, r);
                Grid.SetColumn(cell, c);
                CheckerBoard.Children.Add(cell);
                _cells[r, c] = cell;
            }
        }
    }

    private void ApplyPalette(bool alternate)
    {
        var color1 = alternate ? PaletteB1 : PaletteA1;
        var color2 = alternate ? PaletteB2 : PaletteA2;

        for (var r = 0; r < Rows; r++)
        {
            for (var c = 0; c < Cols; c++)
            {
                var isEven = (r + c) % 2 == 0;
                _cells[r, c].Background = new SolidColorBrush(isEven ? color1 : color2);
            }
        }
    }

    private void UpdateTextPosition()
    {
        if (!_animationEnabled) return;
        var t = (DateTime.Now - _motionStart).TotalSeconds;
        // Small, slow drift: ±12 px horizontal at ~0.35 Hz, ±8 px vertical at ~0.5 Hz.
        var x = 12.0 * Math.Sin(t * 2.2);
        var y = 8.0 * Math.Sin(t * 3.1);
        MovingText.RenderTransform = new TranslateTransform(x, y);
    }

    private void OnToggleAnimationClick(object? sender, RoutedEventArgs e)
    {
        _animationEnabled = !_animationEnabled;
        if (_animationEnabled)
        {
            _boardTimer.Start();
            _motionTimer.Start();
            AnimationToggleButton.Content = "Stop";
        }
        else
        {
            _boardTimer.Stop();
            _motionTimer.Stop();
            MovingText.RenderTransform = new TranslateTransform(0, 0);
            AnimationToggleButton.Content = "Start";
        }
    }
}
