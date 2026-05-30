using System;

namespace Medli;

/// <summary>
/// Date/time formatting off the RTC. Ports the logic of Medli/Kernel/{Date,Time}.cs,
/// but reads <see cref="DateTime.Now"/> (plugged to the RTC in Cosmos gen3 via
/// DateTimePlug) instead of the Medli Legacy SysClock/Clock wrappers. Name arrays are
/// hardcoded to avoid relying on enum reflection under NativeAOT.
/// </summary>
public static class Clock
{
    private static readonly string[] Days =
    { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

    private static readonly string[] Months =
    { "", "January", "February", "March", "April", "May", "June",
      "July", "August", "September", "October", "November", "December" };

    public static string GetDay() => Days[(int)DateTime.Now.DayOfWeek];
    public static string GetMonth() => Months[DateTime.Now.Month];

    public static void PrintDate()
    {
        var n = DateTime.Now;
        Console.WriteLine("The current date is " + GetDay() + " " + n.Day + " of " + GetMonth() + ", " + n.Year);
    }

    public static void PrintTime()
    {
        var n = DateTime.Now;
        Console.WriteLine("The current time is " + n.Hour + ":" + n.Minute + ":" + n.Second);
    }
}
