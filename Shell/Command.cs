using System;

namespace Medli.Apps;

/// <summary>
/// Base class for all shell commands. Ported from the Medli Legacy kernel
/// (Medli/Kernel/Command.cs) — unchanged in shape, just file-scoped namespace.
/// </summary>
public abstract class Command
{
    /// <summary>The word typed at the prompt to invoke this command.</summary>
    public abstract string Name { get; }

    /// <summary>One-line description shown by `help`.</summary>
    public abstract string Summary { get; }

    /// <summary>Runs the command. <paramref name="param"/> is everything after the name.</summary>
    public abstract void Execute(string? param);

    /// <summary>Detailed help for this command; overridable.</summary>
    public virtual void Help()
    {
        Console.WriteLine(Name);
        Console.WriteLine("\t" + Summary);
    }
}
