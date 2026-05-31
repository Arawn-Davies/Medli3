using System;

namespace Medli.Apps;

/// <summary>
/// The 'logout' command — closes applications and logs out. Ported from
/// Medli/Kernel/Utils/System/Logout.cs. Accounts/login aren't ported to gen3 yet, so
/// rather than power the machine off it just starts a fresh session: clears the screen
/// and returns to the prompt. Once login lands this will drop to the login screen.
/// </summary>
public class Logout : Command
{
    public override string Name => "logout";
    public override string Summary => "Closes applications and logs out of the system.";

    public override void Execute(string? param)
    {
        Medli.Kernel._isLoggedIn = false;
        Console.Clear();
        Medli.Kernel.SetColourScheme();
        Console.WriteLine("Logged out. (No account system yet — starting a fresh session.)");
        Console.WriteLine();
    }
}
