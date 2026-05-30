using System;
using System.Collections.Generic;

namespace Medli;

/// <summary>
/// In-memory environment variables. Ported from Medli/Kernel/UserVariables.cs.
/// The file-backed SaveVars/ReadVars are commented out until the FS is ported.
/// </summary>
public static class EnvironmentVariables
{
    public static Dictionary<string, string> usr_var = new Dictionary<string, string>();

    public static void Store(string variable, string contents, bool force)
    {
        if (usr_var.ContainsKey(variable))
        {
            if (force)
            {
                usr_var.Remove(variable);
                contents = contents.Replace(" -u", "");
                usr_var.Add(variable, contents);
            }
            else
            {
                Console.WriteLine("Key already exists!");
            }
        }
        else
        {
            usr_var.Add(variable, contents);
        }
    }

    public static string Retrieve(string variable)
    {
        if (usr_var.ContainsKey(variable))
            return usr_var[variable];
        return "Value not found!";
    }

    public static void PrintVars()
    {
        foreach (var kv in usr_var)
            Console.WriteLine("| Key:" + kv.Key + " | Value: " + kv.Value + "|");
    }

    public static void Clear() => usr_var.Clear();

    // --- file-backed persistence: deferred until the FS subsystem is ported ---
    // public static void ReadVars() { ... File.ReadAllLines(SysFiles.EnvironmentVariables) ... }
    // public static void SaveVars() { ... FS.WriteContents(...) ... }
}
