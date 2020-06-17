using System.Collections.Generic;
using System.IO;
using MacroGuards;

namespace MacroIO
{
    public static class TextReaderExtensions
    {

        /// <summary>
        /// Read all lines one at a time
        /// </summary>
        ///
        public static IEnumerable<string> ReadAllLines(this TextReader reader)
        {
            Guard.NotNull(reader, nameof(reader));
            return ReadAllLinesImpl(reader);
        }


        static IEnumerable<string> ReadAllLinesImpl(TextReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null) yield return line;
        }

    }
}
