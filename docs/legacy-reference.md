---
title: Legacy reference
nav_order: 9
---

# Legacy reference — Medli gen2 ("Medli Legacy")


*63 documented types.*

> **⚠️ Stale.** Auto-generated from the XML doc comments of the **Medli gen2**
> ("Medli Legacy") source under `Medli/` (excluded from the build). Describes the old
> Cosmos gen2 / IL2CPU kernel, **not** Medli3 — names/APIs often differ. A map of what
> exists to port.


## `Common`

- **`AreaInfo`** <small>(class, `Common/AreaInfo.cs`)</small> — Listings of the information areas
- **`KernelArea`** <small>(enum, `Common/AreaInfo.cs`)</small> — Enum of system subcomponents
- **`DeviceAreaInfo`** <small>(class, `Common/AreaInfo.cs`)</small> — Class definition for the device information areas
- **`KernelAreaInfo`** <small>(class, `Common/AreaInfo.cs`)</small> — Class definition for the different kernel areas responsible for each component
- **`Daemon`** <small>(class, `Common/Services/Services.cs`)</small> — class definition for kernel daemons
- **`AccessPriority`** <small>(enum, `Common/Services/Services.cs`)</small> — Class definition for access priorities, etc. core system daemons would have higher priority than others
- **`Kernel`** <small>(class, `Common/VersionInfo.cs`)</small> — Partial class definition for main Medli kernel

## `Core`

- **`Device`** <small>(class, `Core/Device.cs`)</small> — Class definition for Medli-Core Device

## `Hardware`

- **`Clock`** <small>(class, `Hardware/Clock.cs`)</small> — Class definition for Medli-Hardware Clock
- **`DiskListing`** <small>(class, `Hardware/DiskUtility/MDFS.Physical/MFSU.cs`)</small> — Defines a disk listing
- **`PartitionListing`** <small>(class, `Hardware/DiskUtility/MDFS.Physical/MFSU.cs`)</small> — Defines a partition listing
- **`MFSUtility`** <small>(class, `Hardware/DiskUtility/MDFS.Physical/MFSU.cs`)</small> — The Disk Utility class, containing methods and properties
- **`Device`** <small>(class, `Hardware/Drivers/Device.cs`)</small> — Abstract class definition for Medli-Hardware device This class is a stub with no child properties
- **`deviceArea`** <small>(enum, `Hardware/HAL.cs`)</small> — LSPCI listing all PCI devices attached
- **`Now`** <small>(class, `Hardware/RTC.cs`)</small> — NOT RECOMMENDED! Waits for a given amount of ticks. It depends on the CPU speed.
- **`DateFormat`** <small>(enum, `Hardware/RTC.cs`)</small> — DateFormat
- **`TimeFormat`** <small>(enum, `Hardware/RTC.cs`)</small> — TimeFormat

## `Init`

- **`Boot`** <small>(class, `Init/Boot.cs`)</small> — Initial boot class definition including init methods

## `Kernel`

- **`Date`** <small>(class, `Kernel/Date.cs`)</small> — Class definition for Medli Date formatting
- **`AppLauncher`** <small>(class, `Kernel/Utils/Applications/AppLauncher.cs`)</small> — Class definition for AppLauncher
- **`Cpedit`** <small>(class, `Kernel/Utils/Applications/Cocoaedit.cs`)</small> — Cocoapad Editor class contains methods needed for the editor to function
- **`Cowsay`** <small>(class, `Kernel/Utils/Applications/Cowsay.cs`)</small> — Class definition for Cowsay A must-have for any command line operating system
- **`IDE`** <small>(class, `Kernel/Utils/Applications/IDE.cs`)</small> — Cocoapad Development Environment class contains methods needed for the editor to function
- **`Mdscript`** <small>(class, `Kernel/Utils/Applications/Run.cs`)</small> — Class definition for Medliscript (mdscript), a simple scripting interface for the Medli command line shell
- **`Copy`** <small>(class, `Kernel/Utils/Filesystem/Copy.cs`)</small> — Class definition for the 'copy' command
- **`Dir`** <small>(class, `Kernel/Utils/Filesystem/Dir.cs`)</small> — Class definition for the 'dir' command
- **`Move`** <small>(class, `Kernel/Utils/Filesystem/Move.cs`)</small> — Class definition for the 'move' command
- **`cd`** <small>(class, `Kernel/Utils/Filesystem/cd.cs`)</small> — Class definition for the 'cd' command
- **`mkdir`** <small>(class, `Kernel/Utils/Filesystem/mkdir.cs`)</small> — Class definition for the 'mkdir' command
- **`rm`** <small>(class, `Kernel/Utils/Filesystem/rm.cs`)</small> — Class definition for the 'rm' command
- **`Clear`** <small>(class, `Kernel/Utils/Shell/Clear.cs`)</small> — Class definition for the 'clear' command
- **`Date`** <small>(class, `Kernel/Utils/Shell/Date.cs`)</small> — Class definition for the 'date' command
- **`Echo`** <small>(class, `Kernel/Utils/Shell/Echo.cs`)</small> — Class definition for the 'echo' command
- **`Exit`** <small>(class, `Kernel/Utils/Shell/Exit.cs`)</small> — Class definition for the 'exit' command
- **`HelpCommand`** <small>(class, `Kernel/Utils/Shell/Help.cs`)</small> — Class definition for the 'help' command
- **`Pause`** <small>(class, `Kernel/Utils/Shell/Pause.cs`)</small> — Class definition for the 'pause' command
- **`Script`** <small>(class, `Kernel/Utils/Shell/Script.cs`)</small> — Class definition for the 'script' command
- **`Set`** <small>(class, `Kernel/Utils/Shell/Set.cs`)</small> — Class definition of the 'set' command
- **`Time`** <small>(class, `Kernel/Utils/Shell/Time.cs`)</small> — Class definition for the 'time' command
- **`Version`** <small>(class, `Kernel/Utils/Shell/Version.cs`)</small> — Class definition of command 'version'
- **`Get`** <small>(class, `Kernel/Utils/System/Get.cs`)</small> — Class definition for the 'get' command
- **`Logout`** <small>(class, `Kernel/Utils/System/Logout.cs`)</small> — Class definition for the 'reboot' command
- **`Multiscreen`** <small>(class, `Kernel/Utils/System/MultiScreen.cs`)</small> — Class definition for Multiscreen
- **`Panic`** <small>(class, `Kernel/Utils/System/Panic.cs`)</small> — Class definition for the 'panic' command
- **`Reboot`** <small>(class, `Kernel/Utils/System/Reboot.cs`)</small> — Class definition for the 'reboot' command
- **`Shutdown`** <small>(class, `Kernel/Utils/System/Shutdown.cs`)</small> — Class definition for the 'shutdown' command

## `System`

- **`AccountDef`** <small>(class, `System/AccountDef.cs`)</small> — Class definition of type Account
- **`UserType`** <small>(class, `System/AccountDef.cs`)</small> — Class definition of the user levels
- **`MEnvironment`** <small>(class, `System/Environment.cs`)</small> — Will hold the environment methods which will be called by various components in Medli
- **`Effect`** <small>(enum, `System/Framework/Console/Bootscreen.cs`)</small> — BootScreen debugger, I'm getting a stack overflow somewhere...
- **`AConsole`** <small>(class, `System/Framework/Console/Console.cs`)</small> — Medli Framework Console class
- **`VideoBuffer`** <small>(class, `System/Framework/Console/VideoRAM.cs`)</small> — Location of the VGA Video Memory buffer (0xB8000)
- **`MD5`** <small>(class, `System/Framework/Crypto/MD5.cs`)</small> — Thanks to Aurora01!
- **`Digest`** <small>(class, `System/Framework/Crypto/MD5.cs`)</small> — Copies a 512 bit block into X as 16 32 bit words
- **`RockPotato`** <small>(class, `System/Framework/Crypto/RockPotato.cs`)</small> — A hash developed by Splitty
- **`ROT13`** <small>(class, `System/Framework/Crypto/Rot.cs`)</small> — Rot13
- **`ROT26`** <small>(class, `System/Framework/Crypto/Rot.cs`)</small> — Holy cow...
- **`ROT47`** <small>(class, `System/Framework/Crypto/Rot.cs`)</small> — That's a joke, isn't it!?
- **`StringExtensions`** <small>(class, `System/Framework/Extensions.cs`)</small> — Press-any-key prompt with custom text
- **`BinaryReader`** <small>(class, `System/Framework/IO/BinaryReader.cs`)</small> — Class definition for BinaryReader
- **`DateFormat`** <small>(enum, `System/Framework/RTC.cs`)</small> — DateFormat
- **`TimeFormat`** <small>(enum, `System/Framework/RTC.cs`)</small> — TimeFormat
- **`Installer`** <small>(class, `System/Installer.cs`)</small> — Class for the Medli installer
