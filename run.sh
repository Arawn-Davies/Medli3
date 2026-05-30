#!/usr/bin/env bash
# Build the kernel and boot it in QEMU.
#
# Unlike `cosmos run`, this does NOT pass -no-shutdown, so when the kernel calls
# Power.Shutdown() (ACPI S5 soft-off) QEMU exits and the window closes.
#
# Usage:
#   ./run.sh [x64|arm64]      # default: x64
#   MEM=1024 ./run.sh arm64   # override memory (MB)
#   HEADLESS=1 ./run.sh       # serial-only, no display window
set -euo pipefail

ARCH="${1:-x64}"
# Accept common aliases for x86-64.
case "$ARCH" in
  amd64|x86_64|x86-64) ARCH=x64 ;;
  aarch64)             ARCH=arm64 ;;
esac
MEM="${MEM:-512}"
ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT="$ROOT/Medli3"
COSMOS="$HOME/.dotnet/tools/cosmos"
QEMU_DIR="$HOME/.cosmos/tools/qemu"
ISO="$PROJECT/output-$ARCH/Medli3.iso"

# Build for the requested arch.
"$COSMOS" build -a "$ARCH" -p "$PROJECT"

# Common args. -no-reboot is kept (exit on triple-fault); -no-shutdown is
# deliberately omitted so an ACPI power-off actually terminates QEMU.
common=( -L "$HOME/.cosmos/tools/share/qemu" -m "${MEM}M"
         -cdrom "$ISO" -boot d -no-reboot -serial stdio )

case "$ARCH" in
  x64)
    QEMU="$QEMU_DIR/qemu-system-x86_64"
    args=( -M q35 -cpu max "${common[@]}" -vga std )
    ;;
  arm64)
    # virt has no PS/2; Cosmos' arm64 HAL uses a virtio-input keyboard, so we
    # must attach one (and a mouse) or no input device is ever detected.
    #
    # Cosmos only probes the virtio-MMIO transport (it scans the virt board's
    # 32 virtio-mmio slots, not PCIe), so the *-device (MMIO) variants are the
    # ones it can actually bind. INPUT=pci forces the PCIe variants instead.
    QEMU="$QEMU_DIR/qemu-system-aarch64"
    case "${INPUT:-mmio}" in
      mmio) kbd=virtio-keyboard-device; mouse=virtio-mouse-device ;;
      pci)  kbd=virtio-keyboard-pci;    mouse=virtio-mouse-pci ;;
      *) echo "Unknown INPUT: ${INPUT} (use mmio or pci)" >&2; exit 1 ;;
    esac
    args=( -M virt,highmem=off -cpu cortex-a72 -bios edk2-aarch64-code.fd
           "${common[@]}" -device ramfb
           -device "$kbd" -device "$mouse" )
    ;;
  *)
    echo "Unknown arch: $ARCH (use x64 or arm64)" >&2; exit 1 ;;
esac

# Headless => no display window (serial only). Otherwise open a GUI window.
# On macOS the bundled QEMU supports cocoa/sdl; override with DISPLAY_BACKEND.
if [[ "${HEADLESS:-0}" == "1" ]]; then
  args+=( -display none )
else
  args+=( -display "${DISPLAY_BACKEND:-cocoa}" )
fi

exec "$QEMU" "${args[@]}"
