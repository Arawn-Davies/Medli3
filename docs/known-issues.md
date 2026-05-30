---
title: Known issues
nav_order: 7
---

# Known issues

Issues found during the port. The first two are **upstream** (Cosmos gen3), not Medli3
bugs; documented here because they shape what Medli3 can do.

## 1. Per-cell background colour is lost on scroll (upstream)

`KernelConsole` stores a background colour per cell and draws it correctly while
writing, but its **redraw path discards it**. `RedrawInternal()` does a single
`_canvas.Clear(_backgroundColor)` then redraws only each cell's *foreground* glyph —
it never repaints `cell.BackgroundColor`. `Scroll()` calls `RedrawInternal()`, so the
first line-feed that scrolls flattens every cell's background to the global colour.

Symptom: coloured background "swatches" look right until you press Enter near the
bottom of the screen, then revert to the background colour (foreground colours
survive). Confirmed identical in released `v3.0.58` and `main`.

Suggested fix (one spot): make the redraw loop paint the cell background too, or just
delegate to the existing `DrawCharAt(col, row)` which already draws both.

Medli3 works around it by drawing the colour spectrum as **foreground** glyphs.

## 2. PSF unicode glyphs mangled on from-source devkit builds

Non-ASCII glyphs that are reached via the PSF **unicode mapping table** (e.g. `█`,
U+2588) render correctly when building against the **released** `3.0.58` devkit, but
come out as garbage on a **from-source** (`postCreateCommand.sh`) devkit build. ASCII
glyphs (direct index) are fine on both.

The font binary (`DefaultFont.psf`) and the font/console code are **byte-identical**
between the two, so the cause is in the from-source build pipeline (the `ilc` /
`Cosmos.Patcher` / resource-embedding path that the dev script installs as global
tools) mangling the embedded PSF's unicode table — not the kernel's font handling.

Medli3 sidesteps it by using ASCII (`#`) rather than block glyphs in the spectrum.

## 3. ARM64 framebuffer is firmware-locked at 800×600

On `virt` + `ramfb`, the edk2 AAVMF firmware's GOP only advertises 800×600, so Limine
has no other mode to program and a `resolution:` directive in `limine.conf` is ignored.
Higher resolutions would need a `virtio-gpu` device **and** a matching Cosmos driver
(not present), so there's no easy lever today. x86-64 (`-vga std`) is more flexible.

## 4. ARM64 input is virtio-MMIO only

`virt` has no PS/2 controller; Cosmos' ARM64 HAL binds a **virtio-input** keyboard, and
only via the **virtio-MMIO** transport. You must attach `-device virtio-keyboard-device`
(MMIO), not `virtio-keyboard-pci` — the PCIe variant is seen by `PciManager` but never
bound, leaving `keyboards=0`. Handled by `run.sh` (`INPUT=mmio` default). Input arrives
from the QEMU **window**, not the serial terminal.

## 5. Font is large (16×32)

The default spleen PSF cell is 16×32, giving a 50×18 grid at 800×600 (chunky). The
console font is swappable at runtime (`KernelConsole.Default.Font`); a smaller spleen
PSF would increase density. Not yet wired up.
