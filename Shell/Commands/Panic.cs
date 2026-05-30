using System;

namespace Medli.Apps;

/// <summary>
/// The 'panic' command. Ported from Medli/Kernel/Utils/System/Panic.cs.
/// The gen2 version manually fired interrupt 0 via Cosmos.Core.INTs, which has no
/// gen3 equivalent — for now we throw to exercise the kernel's exception handler.
/// </summary>
public class Panic : Command
{
    public override string Name => "panic";
    public override string Summary => "Causes a kernel panic.";

    public override void Execute(string? param)
    {
        // --- gen2 path (no gen3 equivalent): manually raise INT 0 ---
        // var xCtx = new Cosmos.Core.INTs.IRQContext();
        // xCtx.EAX = 0x50; xCtx.EBX = 0x40; xCtx.ECX = 0x30; xCtx.EDX = 0x20;
        // Cosmos.Core.INTs.HandleInterrupt_00(ref xCtx);

        throw new Exception("Manual kernel panic requested via 'panic' command.");
    }
}
