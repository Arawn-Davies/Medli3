---
title: Shell
nav_order: 5
---

# Shell

The shell is `Medli.CommandConsole` — a port of the Medli Legacy `CommandConsole`, slimmed to
drop the Medli Legacy login/screen/filesystem state. It prints a `medli~>` prompt, reads a
line, splits it into a command + argument on the first space, and dispatches to a
registered `Command`.

Each command derives from the abstract `Medli.Apps.Command`:

```csharp
public abstract class Command
{
    public abstract string Name { get; }     // word typed at the prompt
    public abstract string Summary { get; }   // one-line help
    public abstract void Execute(string? param);
    public virtual void Help() { ... }        // detailed help (overridable)
}
```

## Command reference

| Command | Summary |
|---------|---------|
| `help [cmd]` | List commands, or detailed help for one |
| `cls` | Clear the screen |
| `echo <text>` | Print text. `echo $name` prints an environment variable |
| `$ <name> <value>` | Set an environment variable (append `-u` to overwrite) |
| `date` | Current date (from the RTC) |
| `time` | Current time (from the RTC) |
| `version` | Version, build number, and the Cosmos version built against |
| `reboot` | `Power.Reboot()` |
| `shutdown` | Stop the shell → the kernel powers the machine off |
| `panic` | Trigger a kernel panic (currently throws to hit the exception handler) |

### Notes

- **`date`/`time`** read `System.DateTime.Now`, which is plugged to the RTC in Cosmos gen3
  (`DateTimePlug`) and is architecture-neutral — no original-Medli `SysClock`/`Clock` wrappers.
  Day/month names use hardcoded tables (enum reflection is unreliable under NativeAOT).
- **`$` / `echo $var`** use an in-memory `EnvironmentVariables` store. The Medli Legacy
  file-backed `save`/`load` are deferred until the filesystem is ported.
- **`panic`** is a stand-in: the Medli Legacy version fired interrupt 0 via
  `Cosmos.Core.INTs`, which has no Cosmos gen3 equivalent, so it currently `throw`s.

## Adding a command

1. Add `Shell/Commands/Foo.cs` with a `Foo : Command` in `namespace Medli.Apps`.
2. Register it in `CommandConsole`'s constructor: `_commands.Add(new Foo());`
   (keep `HelpCommand` last so it can list everything).

The Medli Legacy shell had ~30 commands (`dir`, `cd`, `copy`, `move`, `rm`, `mkdir`, `fdisk`,
`cowsay`, the VICS editor, …). Most touch the filesystem/disk/hardware and are ported
incrementally — see [Porting](porting.md).
