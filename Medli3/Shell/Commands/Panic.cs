using System;

namespace Medli.Apps;

/// <summary>
/// The 'panic' command. Ported from Medli/Kernel/Utils/System/Panic.cs.
/// The Medli Legacy version manually fired interrupt 0 via Cosmos.Core.INTs, which has no
/// Cosmos gen3 equivalent — and gen3's NativeAOT runtime can't unwind a managed throw yet
/// (it faults before any catch runs), so we display the panic screen directly.
/// </summary>
public class Panic : Command
{
    public override string Name => "panic";
    public override string Summary => "Causes a kernel panic.";

    public override void Execute(string? param)
    {
        // --- Medli Legacy path (no Cosmos gen3 equivalent): manually raise INT 0 ---
        // var xCtx = new Cosmos.Core.INTs.IRQContext();
        // xCtx.EAX = 0x50; xCtx.EBX = 0x40; xCtx.ECX = 0x30; xCtx.EDX = 0x20;
        // Cosmos.Core.INTs.HandleInterrupt_00(ref xCtx);

        // Ideally we'd `throw` and let the shell's try/catch route it to the panic screen,
        // exactly like any unhandled fault. But Cosmos gen3's NativeAOT runtime can't
        // currently unwind a managed throw — it hardware-faults at the throw site and
        // triple-faults (QEMU exits) before any catch runs. So we invoke the panic screen
        // directly. (The try/catch in CommandConsole.Parse stays as a backstop for when
        // upstream exception handling works; then this can go back to a plain throw.)
        Medli.FatalError.Crash("Manual kernel panic", "Requested via the 'panic' command.");
    }
}
