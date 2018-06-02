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


}
}
