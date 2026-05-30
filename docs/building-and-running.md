---
title: Building & running
nav_order: 3
---

# Building & running

## Building

`cosmos build` wraps the .NET build + NativeAOT compile + image packaging.

```bash
cosmos build                 # host architecture (auto-detected)
cosmos build -a x64          # x86-64
cosmos build -a arm64        # ARM64
cosmos build -p <path>       # point at a project dir (handy when cwd differs)
```

Output: `output-<arch>/Medli2.iso`.

The kernel targets `net10.0` and references the gen3 packages (see `Medli2.csproj`):

```xml
<Project Sdk="Cosmos.Sdk/3.0.58">
  <PackageReference Include="Cosmos.Kernel" Version="3.0.58" />
  <PackageReference Include="Cosmos.Kernel.System" Version="3.0.58" />
```

### Build info

A `GenerateBuildInfo` MSBuild target writes `obj/.../BuildInfo.g.cs` each build with
`BuildYear`, `BuildNumber`, and `CosmosVersion` (the Cosmos package version actually
resolved, read from `project.assets.json`). These surface in the boot banner and the
`version` command, and keep the copyright year current automatically.

## Running

### `run.sh` (recommended)

```bash
./run.sh [x64|arm64]          # builds the arch then boots it in QEMU
```

Aliases: `amd64`/`x86_64` → x64, `aarch64` → arm64. Environment knobs:

| Var | Default | Effect |
|-----|---------|--------|
| `MEM` | `512` | guest RAM in MB |
| `HEADLESS` | `0` | `1` = serial only, no display window |
| `DISPLAY_BACKEND` | `cocoa` | QEMU display (`cocoa`/`sdl`) |
| `INPUT` | `mmio` | ARM64 input transport: `mmio` or `pci` (see below) |

`run.sh` deliberately mirrors `cosmos run`'s QEMU invocation **minus `-no-shutdown`**,
so when the kernel calls `Power.Shutdown()` (ACPI S5) QEMU actually exits and the
window closes. `cosmos run` keeps `-no-shutdown` (it leaves the window open to
preserve final serial output), so the window won't close on `shutdown`.

### x86-64

Boots on QEMU's `q35` machine with `-vga std`. A **PS/2 keyboard** is present by
default, so input "just works".

### ARM64

Boots on QEMU's `virt` machine with UEFI (edk2 AAVMF) and a `ramfb` framebuffer.
Two ARM-specific gotchas, both handled by `run.sh`:

- **No PS/2.** Input must come from a **virtio-input** device, and Cosmos' ARM64 HAL
  only probes the **virtio-MMIO** transport — so `run.sh` attaches
  `-device virtio-keyboard-device` / `virtio-mouse-device` (the MMIO variants).
  The PCIe variants (`INPUT=pci`) are detected by `PciManager` but never bound, so
  you'd get `keyboards=0`. Input comes from the **GUI window**, not the serial terminal.
- **Resolution is firmware-locked at 800×600.** The AAVMF `ramfb` GOP only advertises
  that mode, so a Limine `resolution:` directive is ignored. See
  [Known issues](known-issues.md).

## Building against a from-source Cosmos devkit

To test against your own build of the Cosmos gen3 framework (e.g. to try an upstream
fix), build the framework and point Medli2 at it:

```bash
git clone --recurse-submodules https://github.com/valentinbreiz/nativeaot-patcher
cd nativeaot-patcher
./.devcontainer/postCreateCommand.sh    # packs all Cosmos.* to artifacts/, installs tools
```

Local dev builds are versioned `3.0.58.<yyyyMMdd>`, **not** plain `3.0.58`. Because
`Medli2.csproj` pins exact `3.0.58`, it keeps using the released packages until you
bump it. To consume the source build, set the SDK + package versions to the dated
version (a `global.json` with `msbuild-sdks.Cosmos.Sdk` plus matching
`PackageReference`s) — they must all move together or NuGet errors with `NU1605`.
The `version` command / boot banner's "Built against Cosmos v…" line tells you which
packages a given image actually used.

> **Submodules matter:** the native ACPI library **LAI** is a git submodule. If you
> clone without `--recurse-submodules` (or forget `git submodule update --init
> --recursive`), the source build links with `undefined symbol: lai_*`.
