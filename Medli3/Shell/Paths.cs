namespace Medli;

/// <summary>
/// Minimal path/working-directory state. A stand-in for Medli/Common/Paths.cs until the
/// filesystem is ported — just tracks the shell's current directory (in memory) so the
/// prompt and the filesystem command stubs have something coherent to show. Medli uses
/// DOS-style '\' separators.
/// </summary>
public static class Paths
{
    /// <summary>The shell's current working directory. Defaults to the root.</summary>
    public static string CurrentDirectory = @"\";
}
