using System;

namespace Medli.Apps;

/// <summary>
/// The 'cowsay' command — a little *nix easter egg. Faithful port of
/// Medli/Kernel/Utils/Applications/Cowsay.cs (cow / tux / sheep / confused_sheep / medli).
/// Pure console output, so it ports unchanged apart from the file-scoped namespace.
/// </summary>
public class Cowsay : Command
{
    public override string Name => "cowsay";
    public override string Summary => "A little *nix easter egg.";

    public override void Execute(string? param)
    {
        if (string.IsNullOrEmpty(param))
        {
            Cow("Say something using 'cowsay <message>'");
            Console.WriteLine(@"You can also use 'cowsay -f tux' for penguin, cow, sheep, confused_sheep and medli");
            return;
        }

        string[] args = param.Split(' ');
        if (args[0] == "-f" && args.Length >= 2)
        {
            // Everything after "-f <face> " is the message.
            string message = param.Length > args[0].Length + args[1].Length + 2
                ? param.Remove(0, args[0].Length + args[1].Length + 2)
                : "";
            switch (args[1])
            {
                case "cow": Cow(message); break;
                case "tux": Tux(message); break;
                case "sheep": Sheep(message); break;
                case "confused_sheep": ConfusedSheep(message); break;
                case "medli": Medli(message); break;
                default: Cow(message); break;
            }
        }
        else
        {
            Cow(param);
        }
    }

    /// <summary>Underlines the speech bubble to match the message width.</summary>
    private static void print(string args)
    {
        for (int i = 1; i <= args.Length; i++)
            Console.Write("-");
    }

    private static void Medli(string args)
    {
        Console.Write("/--"); print(args); Console.WriteLine(@"--\");
        Console.WriteLine("|- " + args + @" -|");
        Console.Write("/--"); print(args); Console.WriteLine(@"--\");
        Console.WriteLine(@"
       \
        \
         \    /\
          \  /  \
            /____\
           /\    /\
          /  \  /  \
         /____\/____\
");
    }

    private static void Cow(string args)
    {
        Console.Write("/--"); print(args); Console.WriteLine(@"--\");
        Console.WriteLine("|- " + args + @" -|");
        Console.Write(@"\--"); print(args); Console.Write(@"--/");
        Console.WriteLine(@"
       \
        \   ^__^
         \  (oo)\_______
            (__)\       )\/\
                ||----w |
                ||     ||");
    }

    private static void Tux(string args)
    {
        Console.Write("/--"); print(args); Console.WriteLine(@"--\");
        Console.WriteLine("|- " + args + @" -|");
        Console.Write(@"\--"); print(args); Console.Write(@"--/");
        Console.WriteLine(@"
       \
        \
         \     .--.
          \   |o_o |
              |:_/ |
             //   \ \
            (|     | )
           /'\_   _/`\
           \___)=(___/
");
    }

    private static void Sheep(string args)
    {
        Console.Write("/--"); print(args); Console.WriteLine(@"--\");
        Console.WriteLine("|- " + args + @" -|");
        Console.Write(@"\--"); print(args); Console.Write(@"--/");
        Console.WriteLine(@"
  \
   \
    \
     \
       __
      UooU\./@@@@@@\,
      \__/(@@@@@@@@@@)
           (@@@@@@@@)
           `YY~~~~YY'
            ||    ||
");
    }

    private static void ConfusedSheep(string args)
    {
        Console.Write("/--"); print(args); Console.WriteLine(@"--\");
        Console.WriteLine("|- " + args.ToUpper() + @" -|");
        Console.Write(@"\--"); print(args); Console.Write(@"--/");
        Console.WriteLine(@"
  \                 __
   \               (oo)
    \              (  )
     \             /--\
       __         / \  \
      UooU\.'@@@@@@`.\  )
      \__/(@@@@@@@@@@) /
           (@@@@@@@@)((
           `YY~~~~YY' \\
            ||    ||   >>
");
    }
}
