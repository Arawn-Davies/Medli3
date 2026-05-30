---
title: About
nav_order: 8
---

# About Medli & Makar

Medli is a hobby operating system by **Arawn Davies**. It has a
sibling, **Makar** — *parallel, independent implementations of the same OS concept*,
not layers of one another.

| | Makar | Medli |
|---|---|---|
| Language | C (`i686-elf-gcc`) | C# (Cosmos) |
| Runtime | bare metal (manual PMM/paging/heap) | managed kernel + GC |
| Path separator | `/` (Unix-style) | `\` (DOS-style, historically) |

They share command vocabulary, a daemon/service model, a vi-style editor (**VICS** on
Medli — "vi C-Sharp" — ported to Makar as **VIX**), a common filesystem layout target,
and eventually shared binary formats (the proposed **MXF** for native interop; **MEF**
stays Medli-only; **COM** as the DOS-flat lowest common denominator).

- Makar: <https://github.com/Arawn-Davies/Makar> (BSD-3-Clause Clear)
- Medli (gen2): <https://github.com/Arawn-Davies/Medli>
- Medli2 (gen3, this repo): <https://github.com/Arawn-Davies/Medli2>
- Overview site: <http://arawn-davies.co.uk/Makar/makar-medli.html>

## gen2 → gen3

The original Medli (now "gen2") is built with **Cosmos** via **IL2CPU**, the IL→x86
transpiler. **Medli2** is the "gen3" continuation built on
[Cosmos gen3 / nativeaot-patcher](https://github.com/valentinbreiz/nativeaot-patcher),
which uses the official **.NET NativeAOT** compiler instead — gaining ARM64 alongside
x86-64, and a more standard C# build. The gen2 source is preserved in this repo under
`Medli/` as the porting reference (see [Porting from gen2](porting.md)).

## Credits

- Medli & Makar: Arawn Davies.
- Cosmos gen3 / nativeaot-patcher: Valentin Breiz and contributors.
- The original Cosmos project: the CosmosOS team.
