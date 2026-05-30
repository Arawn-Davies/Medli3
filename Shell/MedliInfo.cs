namespace Medli;

/// <summary>
/// Central place for kernel version/build info. <see cref="BuildYear"/> and
/// <see cref="BuildNumber"/> live in the generated BuildInfo.g.cs (written each build
/// by the GenerateBuildInfo target in Medli2.csproj) — the gen3 equivalent of the
/// gen2 VersionInfo.cs/BuildNumber mechanism.
/// </summary>
public static partial class MedliInfo
{
    public const string Version = "0.1-gen3";

    public static string Copyright => "Copyright (C) " + BuildYear + " Arawn Davies, Siaranite Solutions";
}
