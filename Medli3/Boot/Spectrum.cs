using System;

namespace Medli;

/// <summary>
/// Shows the full 16-colour console palette. New to Medli3 (the Medli Legacy boot
/// had no such demo); useful as a quick check that fg/bg colours render.
/// </summary>
public static class Spectrum
{
    // Names indexed by ConsoleColor value (0-15). Hardcoded rather than using
    // Enum.ToString(), which NativeAOT/Cosmos may not support.
    private static readonly string[] Names =
    {
        "Black", "DarkBlue", "DarkGreen", "DarkCyan",
        "DarkRed", "DarkMagenta", "DarkYellow", "Gray",
        "DarkGray", "Blue", "Green", "Cyan",
        "Red", "Magenta", "Yellow", "White",
    };

    public static void Show()
    {
        var savedFg = Console.ForegroundColor;
        var savedBg = Console.BackgroundColor;

        Console.WriteLine("Colour spectrum:");

        // Foreground swatches: each colour name printed in its own colour.
        for (int i = 0; i < 16; i++)
        {
            Console.ForegroundColor = (ConsoleColor)i;
            Console.Write(Names[i]);
            Console.Write("  ");
        }
        Console.WriteLine();

        // Swatches as FOREGROUND glyphs. Cosmos' KernelConsole keeps each cell's
        // foreground colour across a scroll/redraw but NOT its background colour, so
        // true background swatches vanish the moment the screen scrolls. ASCII '#'
        // because the full-block U+2588 is multi-byte UTF-8 and KernelConsole.Write
        // draws a glyph per byte (→ "EEE" junk) rather than mapping the codepoint.
        for (int i = 0; i < 16; i++)
        {
            Console.ForegroundColor = (ConsoleColor)i;
            Console.Write("## ");
        }
        Console.WriteLine();

        Console.ForegroundColor = savedFg;
        Console.BackgroundColor = savedBg;
    }
}
