---
title: Home
layout: home
nav_order: 1
---

# Medli2 documentation

Medli2 is **Medli ported to Cosmos gen3** — a NativeAOT-compiled bare-metal C#
kernel. It boots on x86-64 and ARM64 into a colour console shell.

## Contents

- **[Getting started](getting-started.md)** — install the toolchain, build, first boot.
- **[Building & running](building-and-running.md)** — the `cosmos` CLI, `run.sh`,
  QEMU details for x64 and ARM64, and building against a from-source Cosmos devkit.
- **[Architecture](architecture.md)** — project layout, the boot flow, and the
  Cosmos gen3 kernel lifecycle.
- **[Shell](shell.md)** — the interactive shell and its command reference.
- **[Porting from gen2](porting.md)** — how the gen2 (IL2CPU) → gen3 (NativeAOT)
  port is structured and what is/isn't done.
- **[Known issues](known-issues.md)** — upstream bugs and platform quirks found
  during the port.
- **[gen2 reference](gen2-reference.md)** — catalogue of the legacy gen2 subsystems,
  taken from their own XML doc comments. **Stale** — describes the old kernel.
- **[About](about.md)** — Medli, the sibling project Makar, and the lineage.

> **A note on "gen2" vs "gen3".** *gen2* is the original Medli: C#/X# compiled by
> **IL2CPU**, on `Cosmos.System2`/`Cosmos.HAL2`. *gen3* is this port: standard C#
> compiled by **NativeAOT** via the Cosmos gen3 SDK (`Cosmos.Kernel*`). The two
> APIs differ structurally, so porting is file-by-file, not a recompile.
