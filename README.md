# Medli3

**Medli3 is the third generation of Medli** (Medli gen3) — a bare-metal C# operating
system built on **Cosmos gen3**, the [NativeAOT-based Cosmos framework](https://github.com/valentinbreiz/nativeaot-patcher)
that compiles a .NET kernel to a bootable image with the official **NativeAOT** compiler.

> **Two separate "gen" numbers — keep them straight:**
> - **Medli** generation = the OS: **gen1** (Medli-Classic) → **gen2** (Medli, "Medli
>   Legacy") → **gen3** (this repo, Medli3).
> - **Cosmos** generation = the framework: **gen2** (IL2CPU transpiler) → **gen3**
>   (NativeAOT).
>
> **Medli3 = Medli gen3, on Cosmos gen3.** (Medli gen2 ran on Cosmos gen2.)

Medli3 boots on **x86-64** and **ARM64**, into a colour console shell.

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

- `Kernel.cs` — kernel entry point (`Medli3.Kernel : Cosmos.Kernel.System.Kernel`).
- `Boot/` — boot visuals: colour scheme, logo/welcome banner, colour spectrum.
- `Shell/` — the interactive shell (`CommandConsole`) and its commands.
- `Medli/` — **Medli gen2 ("Medli Legacy")** source, excluded from the build, kept as the
  primary porting reference.
- `Medli-Classic/` — **Medli gen1** source, also vendored for reference (excluded).
- `src/C/` — placeholder for native C compiled into the kernel (shared Makar/Medli code).
- `run.sh` — QEMU launcher for x64/arm64.

## Lineage

| Gen | Project | Framework | Repo |
|-----|---------|-----------|------|
| gen1 | Medli-Classic | Cosmos (early) | [Medli-Classic](https://github.com/Arawn-Davies/Medli-Classic) |
| gen2 | Medli ("Medli Legacy") | Cosmos gen2 (IL2CPU) | [Medli](https://github.com/Arawn-Davies/Medli) |
| **gen3** | **Medli3** (this repo) | **Cosmos gen3** (NativeAOT) | [Medli3](https://github.com/Arawn-Davies/Medli3) |

Medli is also paralleled by [Makar](https://github.com/Arawn-Davies/Makar) — a
ground-up implementation of the same OS concept in C. See [docs/about.md](docs/about.md).

## Documentation

| Doc | Contents |
|-----|----------|
| [Getting started](docs/getting-started.md) | Toolchain install, build, first boot |
| [Building & running](docs/building-and-running.md) | `cosmos` CLI, `run.sh`, QEMU, devkit-from-source |
| [Architecture](docs/architecture.md) | Project layout, boot flow, kernel lifecycle |
| [Shell](docs/shell.md) | The shell and command reference |
| [Porting](docs/porting.md) | How the Medli gen2 → Medli3 port works |
| [Known issues](docs/known-issues.md) | Upstream bugs and platform quirks found so far |
| [Legacy reference](docs/legacy-reference.md) | Catalogue of Medli gen2 (Medli Legacy) subsystems (stale) |
| [Classic reference](docs/classic-reference.md) | Catalogue of Medli gen1 (Medli-Classic) subsystems (stale) |
| [About](docs/about.md) | Medli, Makar, and the gen lineage |

## Licence

Released under the BSD-3-Clause-Clear licence (matching the wider Medli/Makar family).
