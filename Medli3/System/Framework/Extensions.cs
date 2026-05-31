using System.Text;

namespace Medli.System.Framework;

/// <summary>
/// Numeric/byte helpers ported from Medli/System/Framework/Extensions.cs. Trimmed to the
/// members the ported crypto needs (the timer/Hz helpers depended on gen2 HAL and aren't
/// ported). <see cref="Hex"/> renders a byte array as an uppercase hex string.
/// </summary>
public static class NumericExtensions
{
    public static string Hex(this byte[] value)
    {
        int offset = 0;
        int length = value.Length - offset;
        var builder = new StringBuilder(length * 2);
        int b;
        for (int i = offset; i < length + offset; i++)
        {
            b = value[i] >> 4;
            builder.Append((char)(55 + b + (b - 10 >> 31 & -7)));
            b = value[i] & 0xF;
            builder.Append((char)(55 + b + (b - 10 >> 31 & -7)));
        }
        return builder.ToString();
    }
}
