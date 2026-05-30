# Medli2

**Medli ported to Cosmos gen3** — a bare-metal C# operating system built on
[Cosmos gen3](https://github.com/valentinbreiz/nativeaot-patcher), which compiles a
.NET kernel to a bootable image with **NativeAOT** (replacing the old IL2CPU
transpiler used by gen2 Medli).

Medli2 boots on **x86-64** and **ARM64**, into a colour console shell.

```
   __    _
   /|   / |            |  |  o       //\\
  / |  /  |  ___    ___|  |  |      //__\\
 /  | /   | |___|  |   |  |  |     //\++/\\
/   |/    | |___   |___|  |_ |    //__\/__\\
                                  ~~~~~~~~~~
The C# free and open source Operating System
```

## Quick start

```bash
# one-time toolchain install (.NET 10 SDK required)
dotnet tool install -g Cosmos.Tools
cosmos install && cosmos check

# build + boot in QEMU
cosmos build           # or: cosmos build -a x64 / -a arm64
./run.sh               # x64 by default; ./run.sh arm64 for ARM
```

See **[docs/](docs/)** for the full guide.

## What's here

- `Kernel.cs` — kernel entry point (`Medli2.Kernel : Cosmos.Kernel.System.Kernel`).
- `Boot/` — boot visuals: colour scheme, logo/welcome banner, colour spectrum.
- `Shell/` — the interactive shell (`CommandConsole`) and its commands.
- `Medli/` — the **legacy gen2 Medli source**, excluded from the build, kept as a
  reference for incremental porting to the gen3 API.
- `run.sh` — QEMU launcher for x64/arm64.

## Lineage

Medli2 is the **gen3** continuation of [Arawn-Davies/Medli](https://github.com/Arawn-Davies/Medli)
(gen2, IL2CPU/Cosmos.System2), and a sibling of [Makar](https://github.com/Arawn-Davies/Makar),
a parallel ground-up implementation of the same OS concept in C. See
[docs/about.md](docs/about.md).

## Documentation

| Doc | Contents |
|-----|----------|
| [Getting started](docs/getting-started.md) | Toolchain install, build, first boot |
| [Building & running](docs/building-and-running.md) | `cosmos` CLI, `run.sh`, QEMU, devkit-from-source |
| [Architecture](docs/architecture.md) | Project layout, boot flow, kernel lifecycle |
| [Shell](docs/shell.md) | The shell and command reference |
| [Porting from gen2](docs/porting.md) | How the gen2 → gen3 port works |
| [Known issues](docs/known-issues.md) | Upstream bugs and platform quirks found so far |
| [About](docs/about.md) | Medli, Makar, and the gen2 lineage |

## Licence

Released under the BSD-3-Clause-Clear licence (matching the wider Medli/Makar family).
