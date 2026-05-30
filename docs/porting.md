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
whole gen2 tree lives under `Medli/` but is excluded from the build; files are reworked
into `Boot/`/`Shell/` as they're ported.

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
- Shell core (`Command`, `CommandConsole`) and commands: `cls`, `echo` (+`$var`), `$`
  (set), `date`, `time`, `version`, `reboot`, `shutdown`, `panic`, `help`.
- In-memory environment variables.
- Build/version info generation.

**Deferred** (need subsystems not yet ported):

- Filesystem (FAT / the custom MDFS & WitchFS), disk drivers (IDE/FDD), `dir`/`cd`/
  `copy`/`move`/`rm`/`mkdir`/`fdisk`.
- The custom `AConsole` + animated `Bootscreen`.
- Networking (RTL8139, serial), crypto, the VICS editor, the menu system, login/accounts.

See the [legacy reference](legacy-reference.md) (gen2) and
[classic reference](classic-reference.md) (gen1) for the full subsystem catalogues.

## Framework quirks carried into the port

A couple of Cosmos gen3 framework quirks affect the port directly — see
[Known issues](known-issues.md): the `KernelConsole` per-cell background not surviving
a scroll, and PSF unicode glyphs mangled on from-source devkit builds.
