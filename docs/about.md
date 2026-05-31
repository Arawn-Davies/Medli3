---
title: About
nav_order: 8
---

# About Medli & Makar

Medli is a hobby operating system by **Arawn Davies**. It has a sibling, **Makar** —
*parallel, independent implementations of the same OS concept*, not layers of one
another.

| | Makar | Medli |
|---|---|---|
| Language | C (`i686-elf-gcc`) | C# (Cosmos) |
| Runtime | bare metal (manual PMM/paging/heap) | managed kernel + GC |
| Path separator | `/` (Unix-style) | `\` (DOS-style, historically) |

They share command vocabulary, a daemon/service model, a vi-style editor (**VICS** on
Medli — "vi C-Sharp" — ported to Makar as **VIX**), a common filesystem layout target,
and eventually shared binary formats (the proposed **MXF** for native interop; **MEF**
stays Medli-only; **COM** as the DOS-flat lowest common denominator).

## Medli generations

Medli has had three generations. "Medli gen" (the OS) and "Cosmos gen" (the framework
it's built on) are *different* numbers — don't conflate them.

| Medli gen | Project | Cosmos framework | Repo |
|-----------|---------|------------------|------|
| gen1 | Medli-Classic | Cosmos (early) | [Medli-Classic](https://github.com/Arawn-Davies/Medli-Classic) |
| gen2 | Medli ("Medli Legacy") | Cosmos gen2 (IL2CPU) | [Medli](https://github.com/Arawn-Davies/Medli) |
| **gen3** | **Medli3** (this repo) | **Cosmos gen3** (NativeAOT) | [Medli3](https://github.com/Arawn-Davies/medli3) |

- **Medli gen1 / Medli-Classic** — the earliest Medli.
- **Medli gen2 / "Medli Legacy"** — built with **Cosmos gen2** via **IL2CPU**, the
  IL→x86 transpiler.
- **Medli gen3 / Medli3** — this repo, built on
  [Cosmos gen3 / nativeaot-patcher](https://github.com/valentinbreiz/nativeaot-patcher),
  which uses the official **.NET NativeAOT** compiler — gaining ARM64 alongside x86-64.

Both predecessors are vendored here as porting references (`Medli-Legacy/` = gen2,
`Medli-Classic/` = gen1), as siblings of the `Medli3/` project. See [Porting](porting.md).

In short: **Medli3 = Medli gen3, on Cosmos gen3.**

- Overview site: <http://arawn-davies.co.uk/Makar/makar-medli.html>
- Makar: <https://github.com/Arawn-Davies/Makar> (BSD-3-Clause Clear)

## Credits

- Medli & Makar: Arawn Davies.
- Cosmos gen3 / nativeaot-patcher: Valentin Breiz and contributors.
- The original Cosmos project: the CosmosOS team.
