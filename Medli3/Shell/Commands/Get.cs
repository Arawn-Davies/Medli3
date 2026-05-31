using System;

namespace Medli.Apps;

/// <summary>
/// The 'get' command — retrieves system information. Ported from
/// Medli/Kernel/Utils/System/Get.cs. The args that need hardware/driver subsystems
/// (RAM stats, lspci/lscpu, filesystem volumes, FS service log) aren't ported to gen3
/// yet, so they report that; <c>host</c> and <c>sysinfo</c> work today.
/// </summary>
public class Get : Command
{
    public override string Name => "get";

    public override string Summary => @"Retrieves system information.
get [arg]
sysinfo     - Medli version / build info
host        - System host name
ram_info    - RAM information           (not ported yet)
ram_used    - Total amount of used RAM  (not ported yet)
ram_free    - Total amount of free RAM  (not ported yet)
ram_total   - Total amount of RAM       (not ported yet)
lscpu       - Lists installed CPU(s)    (not ported yet)
lspci       - Lists PCI devices         (not ported yet)
list_vol(s) - Lists filesystem volumes  (not ported yet)
fs_log      - FileSystem service log    (not ported yet)";

    public override void Execute(string? param)
    {
        switch (param)
        {
            case "sysinfo":
                Console.WriteLine("Medli " + MedliInfo.Version + " (build " + MedliInfo.BuildNumber + ")");
                Console.WriteLine("Built against Cosmos v" + MedliInfo.CosmosVersion);
                Console.WriteLine(MedliInfo.Copyright);
                break;
            case "host":
                Console.WriteLine(Medli.Kernel.Hostname);
                break;
            case "ram_info":
            case "ram_used":
            case "ram_free":
            case "ram_total":
            case "lspci":
            case "lscpu":
            case "list_vol":
            case "list_vols":
            case "fs_log":
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("get " + param + ": not yet implemented — the required driver/filesystem subsystem isn't ported to gen3 yet.");
                Medli.Kernel.SetColourScheme();
                break;
            default:
                Console.WriteLine("Unknown argument. Type 'help get' for the list.");
                break;
        }
    }

    public override void Help()
    {
        Console.WriteLine(Name);
        Console.WriteLine(Summary);
    }
}
