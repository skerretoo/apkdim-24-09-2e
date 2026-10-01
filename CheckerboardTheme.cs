using Avalonia.Media;

namespace drugieokno;

public static class CheckerboardTheme
{
    // Muted, low-contrast palettes — deliberately NOT eye-searing, no saturated red.
    public static readonly Color PaletteA1 = Color.Parse("#7FB3B0"); // muted teal
    public static readonly Color PaletteA2 = Color.Parse("#E8E0D0"); // warm light beige
    public static readonly Color PaletteB1 = Color.Parse("#8AB8D1"); // muted blue-gray
    public static readonly Color PaletteB2 = Color.Parse("#D9D5C9"); // light warm gray

    public static readonly SolidColorBrush BrushA1 = new(PaletteA1);
    public static readonly SolidColorBrush BrushA2 = new(PaletteA2);
    public static readonly SolidColorBrush BrushB1 = new(PaletteB1);
    public static readonly SolidColorBrush BrushB2 = new(PaletteB2);

    public static (SolidColorBrush First, SolidColorBrush Second) GetBrushes(bool alternate)
        => alternate ? (BrushB1, BrushB2) : (BrushA1, BrushA2);
}
