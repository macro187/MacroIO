using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace MacroIO.Tests
{
    [TestClass]
    public class TextReaderExtensionsTests
    {

        [TestMethod]
        public void ReadAllLines_Yields_Correct_Sequence()
        {
            Assert.IsTrue(
                new StringReader(new StringBuilder().AppendLine("a").AppendLine("b").AppendLine("c").ToString())
                    .ReadAllLines()
                    .SequenceEqual(new[] {"a", "b", "c"}));
        }


        [TestMethod]
        public void ReadAllLines_Space_Yields_Single_Line_Space()
        {
            Assert.IsTrue(
                new StringReader(new StringBuilder().AppendLine(" ").ToString())
                    .ReadAllLines()
                    .SequenceEqual(new[] {" "}));
        }


        [TestMethod]
        public void ReadAllLines_Empty_String_Yields_Nothing()
        {
            Assert.IsFalse(new StringReader("").ReadAllLines().Any());
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadAllLines_Null_Throws_ArgumentNullException()
        {
            ((TextReader)null).ReadAllLines();
        }

    }
}
