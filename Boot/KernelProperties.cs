using System;
// using Cosmos.System.Graphics;     // gen2 graphics API — no gen3 equivalent ported yet
// using Sys = Cosmos.System;

namespace Medli;

/// <summary>
/// Colour-scheme half of the kernel state. Ported from Medli/Common/KernelProperties.cs.
/// The gen2 graphics/VM-detection members are commented out until ported to Cosmos gen3.
/// </summary>
public static partial class Kernel
{
    // --- gen2 graphics driver + hypervisor detection: not ported yet ---
    // public enum GFXDriver { VMWareSVGA, VBE, VGA }
    // public static GFXDriver graphicsDriver;
    // public enum Hypervisor { VirtualBox, VMWare, Bochs, QEMU, VirtualPC, BareMetal }
    // public static Hypervisor VM;
    // public static string Host;
    // public static bool IsVirtualised;
    // public static Canvas canvas;

    public static ConsoleColor backgroundColour = ConsoleColor.Black;
    public static ConsoleColor foregroundColour = ConsoleColor.White;

    public static void SetColourScheme()
    {
        Console.BackgroundColor = backgroundColour;
        Console.ForegroundColor = foregroundColour;
    }

    public static void SaveColourScheme()
    {
        backgroundColour = Console.BackgroundColor;
        foregroundColour = Console.ForegroundColor;
        SetColourScheme();
    }
}
