---
title: Getting started
nav_order: 2
---

# Getting started

## Prerequisites

- **.NET SDK 10.0 or later** (`dotnet --version`). The Cosmos gen3 tool and SDK
  target `net10.0`.
- A host that QEMU runs on. On macOS/Linux the Cosmos installer bundles QEMU,
  clang/lld, xorriso and GDB; on Windows use the installer from the Cosmos gen3 releases.

## Install the toolchain

```bash
dotnet tool install -g Cosmos.Tools     # the `cosmos` CLI
cosmos install                          # patcher, templates, VS Code ext, native tools
cosmos check                            # verify everything resolved
```

`cosmos check` should report .NET SDK, clang/lld, xorriso, QEMU (x64 + arm64) and
GDB all present.

> If `cosmos` isn't found afterwards, add the global tools dir to your `PATH`
> (`~/.dotnet/tools`) and restart your shell.

## Build

The kernel project lives in `Medli3/` (the predecessor trees `Medli-Legacy/` and
`Medli-Classic/` are reference only). Build it with `-p` from the repo root, or `cd`
into it first:

```bash
cosmos build -p Medli3              # host architecture (auto-detected)
cosmos build -p Medli3 -a x64       # force x86-64
cosmos build -p Medli3 -a arm64     # force ARM64
# or:  cd Medli3 && cosmos build
```

The bootable ISO lands in `Medli3/output-<arch>/Medli3.iso`. (`./run.sh` does the
build + boot for you.)

## Run

```bash
./run.sh                  # build + boot x64 in QEMU
./run.sh arm64            # ARM64 (also: amd64 / x86_64 / aarch64 aliases)
```

You should see the white-on-blue boot screen (logo, welcome banner, colour
spectrum) and a `medli~>` prompt. Type `help`. Type `shutdown` to power off.

See **[Building & running](building-and-running.md)** for QEMU specifics
(especially ARM64 input) and the `cosmos run` vs `run.sh` difference.
