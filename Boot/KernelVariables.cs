using System;

namespace Medli;

/// <summary>
/// Banner/identity half of the kernel state. Ported from Medli/Common/KernelVariables.cs.
/// Filesystem-path members (pcinfo/usrinfo) are commented out until the FS is ported.
/// </summary>
public static partial class Kernel
{
    /// <summary>The kernel version (sourced from <see cref="MedliInfo"/>).</summary>
    public static string KernelVersion = MedliInfo.Version;

    public static bool _isLive;
    public static bool _isRunning;
    public static bool _isLoggedIn = false;

    /// <summary>The system hostname.</summary>
    public static string Hostname = "medli";

    /// <summary>ASCII logo.</summary>
    public static string Logo = @"
   __    _
   /|   / |            |  |  o       //\\
  / |  /  |  ___    ___|  |  |      //__\\
 /  | /   | |___|  |   |  |  |     //\++/\\
/   |/    | |___   |___|  |_ |    //__\/__\\
                                  ~~~~~~~~~~
The C# free and open source Operating System
";

    /// <summary>The welcome message.</summary>
    public static string Welcome = @"
Welcome to Medli version: " + MedliInfo.Version + ", build: " + MedliInfo.BuildNumber + @"
Built against Cosmos v" + MedliInfo.CosmosVersion + @"
Developed by Arawn Davies
" + MedliInfo.Copyright + @", All Rights Reserved
Released under the BSD-3 Clause Clear license
";

    public static string username = "user";
    public static string pcname = "medli";

    // --- gen2 filesystem paths: not ported until the FS subsystem lands ---
    // public static string pcinfo = Common.Paths.System + MEnvironment.dir_ext + "pcinfo.sys";
    // public static string usrinfo = Common.Paths.System + MEnvironment.dir_ext + "usrinfo.sys";
}
