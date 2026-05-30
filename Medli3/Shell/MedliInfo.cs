namespace Medli;

/// <summary>
/// Central place for kernel version/build info. <see cref="BuildYear"/> and
/// <see cref="BuildNumber"/> live in the generated BuildInfo.g.cs (written each build
/// by the GenerateBuildInfo target in Medli3.csproj) — the Cosmos gen3-era equivalent of the original
/// Medli's VersionInfo.cs/BuildNumber mechanism.
/// </summary>
public static partial class MedliInfo
{
    public const string Version = "0.1";

    public static string Copyright => "Copyright (C) " + BuildYear + " Arawn Davies";
}
