---
title: Architecture
nav_order: 4
---

# Architecture

## Project layout

```
Medli3/
├── Kernel.cs            Entry point: Medli3.Kernel : Cosmos.Kernel.System.Kernel
├── Medli3.csproj        Cosmos.Sdk project + GenerateBuildInfo target
├── Bootloader/
│   └── limine.conf      Limine boot entry
├── Boot/                Boot visuals (namespace Medli)
│   ├── KernelProperties.cs   colour scheme (partial class Kernel)
│   ├── KernelVariables.cs    logo / welcome / version (partial class Kernel)
│   └── Spectrum.cs           16-colour palette demo
├── Shell/               The shell (namespaces Medli / Medli.Apps)
│   ├── MedliInfo.cs          version + copyright (partial; build info generated)
│   ├── Clock.cs              RTC-backed date/time formatting
│   ├── EnvironmentVariables.cs   in-memory env vars
│   ├── Command.cs            abstract command base
│   ├── CommandConsole.cs     REPL + parser + command registry
│   └── Commands/             cls, echo, set, date, time, version, reboot, shutdown, panic, help
├── Medli/               Medli gen2 ("Medli Legacy") source — EXCLUDED from the build
├── Medli-Classic/       Medli gen1 source — EXCLUDED from the build
├── src/C/               native C compiled into the kernel (shared Makar/Medli code)
└── run.sh               QEMU launcher
```

Both predecessor trees are excluded in `Medli3.csproj`:

```xml
<Compile Remove="Medli/**/*.cs" />
<None    Remove="Medli/**/*" />
<Compile Remove="Medli-Classic/**/*.cs" />
<None    Remove="Medli-Classic/**/*" />
```

Ported files are reintroduced under `Boot/` and `Shell/` reusing the original
`Medli` / `Medli.Apps` namespaces, so future ports drop in with minimal edits.

## Kernel lifecycle

Cosmos gen3 calls into `Medli3.Kernel` (subclass of `Cosmos.Kernel.System.Kernel`):

- **`BeforeRun()`** — once, at startup. Medli3 sets white-on-blue, clears, prints the
  logo + welcome banner + colour spectrum.
- **`Run()`** — the kernel's main loop body. Medli3 hands control to the shell
  (`CommandConsole.Run()`), which owns its own read-eval-print loop and returns when
  `shutdown` runs; the kernel then calls `Power.Shutdown()`.

Power control lives in `Cosmos.Kernel.System.Power`: `Shutdown()`, `Reboot()`,
`Halt()`. `Shutdown()`/`Reboot()` are `[DoesNotReturn]` (ACPI S5 / reset). Note
`Kernel.Stop()` only parks the CPU — it does **not** power the machine off; use
`Power.Shutdown()` for that.

## Boot flow (what you see)

1. Firmware (UEFI on ARM64 / SeaBIOS-style on x64) loads **Limine**, which loads the
   kernel ELF per `Bootloader/limine.conf`.
2. Cosmos native init: CPU, ACPI, heap/GC, HAL, interrupts, PCI, timers, RTC, input,
   scheduler — all logged to **serial** (`COM1` / UART), visible in your terminal.
3. `KernelConsole` initialises on the framebuffer; the managed kernel starts and
   `BeforeRun()`/`Run()` are called. Console output now goes to the **framebuffer**.

So during boot the **serial terminal** shows the native log, while the **QEMU window**
shows the Cosmos console (firmware text early on, then the Medli boot screen).

## Console & colour

`System.Console` is backed by `Cosmos.Kernel.System.Graphics.KernelConsole`
(`KernelConsole.Default`). It supports `ForegroundColor`/`BackgroundColor`
(`ConsoleColor`), `Clear`, `ReadLine`, etc. The font is a **spleen** PSF
(`PCScreenFont.DefaultFont`); the console font can be swapped at runtime via the
public `KernelConsole.Default.Font` setter.

See [Known issues](known-issues.md) for two console caveats found during the port
(per-cell background on scroll; PSF unicode glyphs on from-source builds).
