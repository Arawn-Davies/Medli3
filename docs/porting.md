---
title: Porting
nav_order: 6
---

# Porting (Medli gen2 → Medli3)

Medli3 (Medli gen3) is a port of **Medli gen2** ("Medli Legacy"). The move isn't a
recompile: gen2 ran on **Cosmos gen2** (the IL2CPU transpiler) and Medli3 runs on
**Cosmos gen3** (NativeAOT), and the two Cosmos APIs differ structurally.

(Medli gen1 / Medli-Classic is vendored too, as deeper reference, but the active port
is from gen2.)

## What changed under the hood

| | Medli gen2 ("Medli Legacy") | Medli3 (gen3) |
|---|---|---|
| Cosmos framework | **gen2** (IL2CPU, IL → x86) | **gen3** (NativeAOT, `ilc`) |
| Kernel API | `Cosmos.System2`, `Cosmos.HAL2`, plugs | `Cosmos.Kernel*` |
| Base class | `Cosmos.System.Kernel` | `Cosmos.Kernel.System.Kernel` |
| Target | x86 only | x86-64 **and** ARM64 |
| TFM | `net6.0` | `net10.0` |
| Plugs | `IL2CPU.API` plugs | Cosmos gen3 plug model (different) |

Because the API surface is structurally different, porting is **file-by-file**. The
whole gen2 tree lives under `Medli-Legacy/` (a sibling of the project, so it isn't
compiled); files are reworked into `Medli3/Boot/`/`Medli3/Shell/` as they're ported.

## Conventions

- Ported code keeps the original `Medli` / `Medli.Apps` namespaces so copied files need
  minimal edits.
- gen2 code with no Cosmos gen3 equivalent yet is **commented out in place** with a
  note, rather than deleted — so the original intent stays visible. (e.g. the `Canvas`,
  `GFXDriver`, hypervisor-detection members in `KernelProperties`; the file-backed env
  var persistence; the `panic` interrupt path.)
- Hardware/RTC access prefers **plugged BCL APIs** where they exist — e.g. `date`/`time`
  use `DateTime.Now` (plugged to the RTC) instead of the Medli Legacy `Clock`/`SysClock`
  HAL wrappers, which keeps the code architecture-neutral.

## Status

**Ported:**

- Boot screen: white-on-blue colour scheme, ASCII logo, welcome banner, colour spectrum.
- A smaller embedded console font (spleen 8×16) so wide output fits — see
  [Known issues](known-issues.md).
- Shell core (`Command`, `CommandConsole`) and a `user@host dir~>` prompt.
- Working commands: `cls`, `echo` (+`$var`), `$` (set), `get` (`host`/`sysinfo`),
  `date`, `time`, `version`, `cowsay`, `colour`, `pause`, `md5`, `sha256`,
  `reboot`, `shutdown`, `exit`, `logout`, `panic`, `help`.
- Crypto: MD5 and SHA-256 (pure-logic port), exposed as the `md5`/`sha256` commands.
- In-memory environment variables; build/version info generation.

**Stubbed** — the command exists and `help` lists it, but it reports "not yet
implemented" until its subsystem is ported:

- Filesystem: `dir`, `cd`, `mkdir`, `rm`, `copy`, `move`, `run`, `cpview`, `cpedit`.
- Applications: `vics` (the VICS editor), `launch`, `screen`, `devenv`.
- Disk/hardware: `fdisk`; the `get` hardware args (`ram_*`, `lspci`, `lscpu`,
  `list_vol(s)`, `fs_log`).

**Deferred** (no command surface yet):

- Filesystem (FAT / the custom MDFS & WitchFS), disk drivers (IDE/FDD).
- The custom `AConsole` + animated `Bootscreen`.
- Networking (RTL8139, serial), the menu system, login/accounts. No PC speaker in gen3,
  so anything needing `beep` stays out.

See the [legacy reference](legacy-reference.md) (gen2) and
[classic reference](classic-reference.md) (gen1) for the full subsystem catalogues.

## Framework quirks carried into the port

A couple of Cosmos gen3 framework quirks affect the port directly — see
[Known issues](known-issues.md): the `KernelConsole` per-cell background not surviving
a scroll, and PSF unicode glyphs mangled on from-source devkit builds.
