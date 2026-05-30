---
title: Porting from gen2
nav_order: 6
---

# Porting from gen2

## What changed between gen2 and gen3

| | gen2 Medli | gen3 Medli2 |
|---|---|---|
| Compiler | IL2CPU (IL → x86 transpiler) | **NativeAOT** (`ilc`) |
| Kernel API | `Cosmos.System2`, `Cosmos.HAL2`, plugs | `Cosmos.Kernel*` |
| Base class | `Cosmos.System.Kernel` | `Cosmos.Kernel.System.Kernel` |
| Target | x86 only | x86-64 **and** ARM64 |
| TFM | `net6.0` | `net10.0` |
| Plugs | `IL2CPU.API` plugs | gen3 plug model (different) |

Because the API surface is structurally different, porting is **file-by-file**, not a
recompile. The whole gen2 tree lives under `Medli/` but is excluded from the build;
files are reworked into `Boot/`/`Shell/` as they're ported.

## Conventions

- Ported code keeps the original `Medli` / `Medli.Apps` namespaces so copied files need
  minimal edits.
- gen2 code that has no gen3 equivalent yet is **commented out in place** with a note,
  rather than deleted — so the original intent stays visible. (e.g. the `Canvas`,
  `GFXDriver`, hypervisor-detection members in `KernelProperties`; the file-backed env
  var persistence; the `panic` interrupt path.)
- Hardware/RTC access prefers **plugged BCL APIs** where they exist — e.g. `date`/`time`
  use `DateTime.Now` (plugged to the RTC) instead of the gen2 `Clock`/`SysClock` HAL
  wrappers, which keeps the code architecture-neutral.

## Status

**Ported:**

- Boot screen: white-on-blue colour scheme, ASCII logo, welcome banner, colour spectrum.
- Shell core (`Command`, `CommandConsole`) and commands: `cls`, `echo` (+`$var`), `$`
  (set), `date`, `time`, `version`, `reboot`, `shutdown`, `panic`, `help`.
- In-memory environment variables.
- Build/version info generation.

**Deferred** (need subsystems not yet ported):

- Filesystem (FAT / the custom MDFS & WitchFS), disk drivers (IDE/FDD), `dir`/`cd`/
  `copy`/`move`/`rm`/`mkdir`/`fdisk`.
- The custom `AConsole` + animated `Bootscreen`.
- Networking (RTL8139, serial), crypto, the VICS editor, the menu system, login/accounts.

See the [gen2 reference](gen2-reference.md) for the full catalogue of legacy subsystems.

## Known blockers carried from the framework

A couple of gen3 framework quirks affect the port directly — see
[Known issues](known-issues.md): the `KernelConsole` per-cell background not surviving
a scroll, and PSF unicode glyphs mangled on from-source devkit builds.
