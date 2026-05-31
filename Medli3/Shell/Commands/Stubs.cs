using System;

namespace Medli.Apps;

/// <summary>
/// Why a stubbed command isn't available yet — selects the explanatory message.
/// </summary>
public enum StubReason
{
    /// <summary>Needs the (not-yet-ported) filesystem.</summary>
    Filesystem,
    /// <summary>Needs (not-yet-ported) disk/hardware drivers.</summary>
    Hardware,
    /// <summary>The application itself hasn't been ported yet.</summary>
    Application,
}

/// <summary>
/// A placeholder for a Medli Legacy command whose backing subsystem isn't ported to gen3
/// yet. It keeps the real command's name and summary (so `help` shows the full Medli
/// surface) but, when run, explains that it's not implemented rather than doing nothing.
/// Replace each instance with a real command file as the subsystem is ported.
/// </summary>
public class StubCommand : Command
{
    private readonly string _name;
    private readonly string _summary;
    private readonly StubReason _reason;

    public StubCommand(string name, string summary, StubReason reason)
    {
        _name = name;
        _summary = summary;
        _reason = reason;
    }

    public override string Name => _name;
    public override string Summary => _summary;

    public override void Execute(string? param)
    {
        string why = _reason switch
        {
            StubReason.Filesystem => "the filesystem isn't ported to gen3 yet",
            StubReason.Hardware => "the disk/hardware drivers aren't ported to gen3 yet",
            _ => "this application isn't ported to gen3 yet",
        };

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(_name + ": not yet implemented — " + why + ".");
        Medli.Kernel.SetColourScheme();
    }
}
