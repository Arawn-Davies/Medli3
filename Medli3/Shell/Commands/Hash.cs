using System;
using Medli.System.Framework.Crypto;

namespace Medli.Apps;

/// <summary>
/// The 'md5' / 'sha256' commands — hash the supplied text. New to Medli3 (the gen2 crypto
/// classes existed but weren't exposed as commands); uses the ported
/// <see cref="Medli.System.Framework.Crypto.MD5"/> / <see cref="SHA256"/> implementations.
/// </summary>
public class Md5Command : Command
{
    public override string Name => "md5";
    public override string Summary => "Prints the MD5 hash of the supplied text.";

    public override void Execute(string? param)
    {
        if (string.IsNullOrEmpty(param))
        {
            Console.WriteLine("Usage: md5 <text>");
            return;
        }
        Console.WriteLine(MD5.Hash(param));
    }
}

public class Sha256Command : Command
{
    public override string Name => "sha256";
    public override string Summary => "Prints the SHA-256 hash of the supplied text.";

    public override void Execute(string? param)
    {
        if (string.IsNullOrEmpty(param))
        {
            Console.WriteLine("Usage: sha256 <text>");
            return;
        }
        Console.WriteLine(SHA256.Hash(param));
    }
}
