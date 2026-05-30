---
title: Home
layout: home
nav_order: 1
---

# Medli3 documentation

**Medli3 is Medli gen3** — the third generation of Medli — built on **Cosmos gen3**, a
NativeAOT-compiled bare-metal C# kernel. It boots on x86-64 and ARM64 into a colour
console shell.

> **Mind the two "gens".**
> - **Medli** gen = the OS version: **gen1** (Medli-Classic) → **gen2** (Medli, aka
>   "Medli Legacy") → **gen3** (Medli3, this repo).
> - **Cosmos** gen = the framework: **gen2** (IL2CPU transpiler, `Cosmos.System2`/
>   `Cosmos.HAL2`) → **gen3** (NativeAOT, `Cosmos.Kernel*`).
>
> So Medli gen2 ran on **Cosmos gen2**; **Medli3 runs on Cosmos gen3**. Because the two
> Cosmos APIs differ structurally, porting is file-by-file, not a recompile.

## Contents

- **[Getting started](getting-started.md)** — install the toolchain, build, first boot.
- **[Building & running](building-and-running.md)** — the `cosmos` CLI, `run.sh`,
  QEMU details for x64 and ARM64, and building against a from-source Cosmos devkit.
- **[Architecture](architecture.md)** — project layout, the boot flow, and the
  Cosmos gen3 kernel lifecycle.
- **[Shell](shell.md)** — the interactive shell and its command reference.
- **[Porting](porting.md)** — how the Medli gen2 → Medli3 port is structured and
  what is/isn't done.
- **[Known issues](known-issues.md)** — upstream (Cosmos gen3) bugs and platform
  quirks found during the port.
- **[Legacy reference](legacy-reference.md)** — catalogue of the **Medli gen2** ("Medli
  Legacy") subsystems, from their XML doc comments. **Stale.**
- **[Classic reference](classic-reference.md)** — catalogue of the **Medli gen1**
  (Medli-Classic) subsystems. **Stale.**
- **[About](about.md)** — Medli, the sibling project Makar, and the lineage.
