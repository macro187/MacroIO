using System;
using static System.FormattableString;
using System.IO;
using System.Linq;
using MacroGuards;

namespace MacroIO
{
    public static class PathExtensions
    {

        /// <summary>
        /// Split a path into its components
        /// </summary>
        ///
        /// <remarks>
        /// <para>
        /// Path components are split at either <see cref="Path.DirectorySeparatorChar"/> or
        /// <see cref="Path.AltDirectorySeparatorChar"/>.
        /// </para>
        /// <para>
        /// Empty path components are discarded.  This includes those before an initial separator, after a final
        /// separator, or between two consecutive separators.
        /// </para>
        /// </remarks>
        ///
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>
        /// </exception>
        ///
        public static string[] Split(string path)
        {
            Guard.NotNull(path, nameof(path));

            return path.Split(
                new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar },
                StringSplitOptions.RemoveEmptyEntries);
        }


        /// <summary>
        /// Determine whether one path is a descendant of another
        /// </summary>
        ///
        /// <remarks>
        /// Only the path components themselves are considered.  Whether the path is absolute/relative is ignored.  Path
        /// comparisons are case-sensitive.
        /// </remarks>
        ///
        /// <param name="path">
        /// The path to consider
        /// </param>
        ///
        /// <param name="possibleAncestorPath">
        /// Another path that may be an ancestor of <paramref name="path"/>
        /// </param>
        ///
        /// <returns>
        /// Whether <paramref name="path"/> is a descendant of <paramref name="possibleAncestorPath"/>
        /// </returns>
        ///
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>
        /// - OR -
        /// <paramref name="possibleAncestorPath"/> is <c>null</c>
        /// </exception>
        ///
        public static bool IsDescendantOf(string path, string possibleAncestorPath)
        {
            Guard.NotNull(path, nameof(path));
            Guard.NotNull(possibleAncestorPath, nameof(possibleAncestorPath));

            var components = Split(path);
            var ancestorComponents = Split(possibleAncestorPath);

            if (components.Length <= ancestorComponents.Length) return false;

            return ancestorComponents.SequenceEqual(
                components.Take(ancestorComponents.Length),
                StringComparer.Ordinal);
        }


        /// <summary>
        /// Get the relative path from an ancestor path to a descendant path
        /// </summary>
        ///
        /// <remarks>
        /// Result paths use <see cref="Path.DirectorySeparatorChar"/>
        /// </remarks>
        ///
        /// <param name="path">
        /// A path
        /// </param>
        ///
        /// <param name="ancestorPath">
        /// Path to an ancestor of <paramref name="path"/>
        /// </param>
        ///
        /// <returns>
        /// Relative path from <paramref name="ancestorPath"/> to <paramref name="path"/>
        /// </returns>
        ///
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <c>null</c>
        /// - OR -
        /// <paramref name="ancestorPath"/> is <c>null</c>
        /// </exception>
        ///
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is not a descendant of <paramref name="ancestorPath"/>
        /// </exception>
        ///
        public static string GetPathFromAncestor(string path, string ancestorPath)
        {
            Guard.NotNull(path, nameof(path));
            Guard.NotNull(ancestorPath, nameof(ancestorPath));

            if (!IsDescendantOf(path, ancestorPath))
                throw new ArgumentException(
                    Invariant($"{nameof(path)} is not a descendant of {nameof(ancestorPath)}"),
                    nameof(path));

            return Path.Combine(Split(path).Skip(Split(ancestorPath).Length).ToArray());
        }

    }
}
