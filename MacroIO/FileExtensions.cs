using System.IO;
using MacroGuards;
using MacroSystem;


namespace
MacroIO
{


public static class
FileExtensions
{


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
    var bytes = new byte[3];
    stream.Read(bytes, 0, 3);
    return bytes[0] == '\xEF' && bytes[1] == '\xBB' && bytes[2] == '\xBF';
}


}
}
