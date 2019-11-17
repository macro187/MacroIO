using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MacroGuards;
using MacroSystem;


namespace
MacroIO
{


public static class
FileExtensions
{


/// <summary>
/// Sequence of bytes comprising the UTF-8 byte order mark
/// </summary>
///
public static readonly IReadOnlyList<byte>
Utf8Bom = new[] { (byte)0xEF, (byte)0xBB, (byte)0xBF };


/// <summary>
/// Append lines of text to a file
/// </summary>
///
/// <remarks>
/// <para>
/// If the file already exists, its existing line ending and BOM conventions take precedence.  Otherwise, native line
/// endings are used and no UTF-8 bom is written.
/// </para>
/// <para>
/// Line ending character sequences within individual <paramref name="lines"/> are normalised to be consistent with the
/// rest of the file.
/// </para>
/// </remarks>
///
/// <exception cref="ArgumentNullException">
/// <paramref name="path"/> is <c>null</c>
/// - OR -
/// <paramref name="lines"/> is <c>null</c>
/// </exception>
///
/// <exception cref="ArgumentException">
/// <paramref name="path"/> is empty or whitespace-only
/// </exception>
///
public static void
AppendLines(string path, params string[] lines)
{
    var existingLines = File.ReadAllLines(path);
    RewriteAllLines(path, existingLines.Concat(lines));
}


/// <summary>
/// Create or replace a text file with specified lines of text
/// </summary>
///
/// <remarks>
/// <para>
/// If the file already exists, its existing line ending and BOM conventions take precedence.  Otherwise, native line
/// endings are used and no UTF-8 bom is written.
/// </para>
/// <para>
/// Line ending character sequences within individual <paramref name="lines"/> are normalised to be consistent with the
/// rest of the file.
/// </para>
/// </remarks>
///
/// <exception cref="ArgumentNullException">
/// <paramref name="path"/> is <c>null</c>
/// - OR -
/// <paramref name="lines"/> is <c>null</c>
/// </exception>
///
/// <exception cref="ArgumentException">
/// <paramref name="path"/> is empty or whitespace-only
/// </exception>
///
public static void
RewriteAllLines(string path, IEnumerable<string> lines)
{
    RewriteAllLines(path, lines, LineEnding.Native, false);
}


/// <summary>
/// Create or replace a text file with specified lines of text
/// </summary>
///
/// <remarks>
/// <para>
/// If the file already exists, its existing line ending and BOM conventions take precedence.
/// </para>
/// <para>
/// Line ending character sequences within individual <paramref name="lines"/> are normalised to be consistent with the
/// rest of the file.
/// </para>
/// </remarks>
///
/// <exception cref="ArgumentNullException">
/// <paramref name="path"/> is <c>null</c>
/// - OR -
/// <paramref name="lines"/> is <c>null</c>
/// </exception>
///
/// <exception cref="ArgumentException">
/// <paramref name="path"/> is empty or whitespace-only
/// </exception>
///
public static void
RewriteAllLines(string path, IEnumerable<string> lines, LineEnding lineEnding, bool withBom)
{
    Guard.Required(path, nameof(path));
    Guard.NotNull(lines, nameof(lines));

    if (File.Exists(path))
    {
        lineEnding = DetectLineEndings(path) ?? lineEnding;
        withBom = StartsWithUtf8Bom(path);
    }

    var encoding = new UTF8Encoding(withBom);

    using (var writer = new StreamWriter(path, false, encoding) { NewLine = lineEnding })
        foreach (var line in lines)
            writer.WriteLine(StringExtensions.NormaliseLineEndings(line, lineEnding));
}


/// <summary>
/// Detect the kind of line endings in a text file
/// </summary>
///
/// <returns>
/// The first kind of line ending encountered in the file
/// - OR -
/// <c>null</c> if the file contains no line endings
/// - OR -
/// <c>null</c> if the file does not exist
/// </returns>
///
/// <exception cref="ArgumentNullException">
/// <paramref name="path"/> is <c>null</c>
/// </exception>
///
/// <exception cref="Argument">
/// <paramref name="path"/> is empty or whitespace-only
/// </exception>
///
public static LineEnding
DetectLineEndings(string path)
{
    Guard.Required(path, nameof(path));
    if (!File.Exists(path)) return null;
    using (var text = File.OpenText(path)) return DetectLineEndings(text);
}


/// <summary>
/// Detect the kind of line endings in a stream of text
/// </summary>
///
/// <returns>
/// The first kind of line ending encountered in the text
/// - OR -
/// <c>null</c> if the text contains no line endings
/// </returns>
///
/// <exception cref="ArgumentNullException">
/// <paramref name="text"/> is <c>null</c>
/// </exception>
///
public static LineEnding
DetectLineEndings(TextReader text)
{
    Guard.NotNull(text, nameof(text));

    int lastChar = 0x0000;
    for (;;)
    {
        var thisChar = text.Read();
        if (lastChar == 0x0D && thisChar == 0x0A) return LineEnding.CRLF;
        if (lastChar == 0x0D) return LineEnding.CR;
        if (thisChar == 0x0A) return LineEnding.LF;
        if (thisChar == -1) break;
        lastChar = thisChar;
    }

    return null;
}


/// <summary>
/// Determine whether there is a UTF-8 BOM sequence at the beginning of a file
/// </summary>
///
public static bool
StartsWithUtf8Bom(string path)
{
    Guard.Required(path, nameof(path));
    using (var stream = File.OpenRead(path)) return StartsWithUtf8Bom(stream);
}


/// <summary>
/// Determine whether there is a UTF-8 BOM sequence at the beginning of a stream
/// </summary>
///
public static bool
StartsWithUtf8Bom(Stream stream)
{
    Guard.NotNull(stream, nameof(stream));

    var bytes = new byte[Utf8Bom.Count];
    var bytesRead = stream.Read(bytes, 0, Utf8Bom.Count);

    return
        bytesRead == Utf8Bom.Count &&
        bytes.SequenceEqual(Utf8Bom);
}


}
}
