using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace
MacroIO.Tests.FileExtensionsTests
{


[TestClass]
public class
StartsWithUtf8BomTests
{


static readonly byte[] Bom =                new[] { (byte)0xEF, (byte)0xBB, (byte)0xBF, };
static readonly byte[] PartialBom =         new[] { (byte)0xEF, (byte)0xBB, };
static readonly byte[] OtherBytes =         new[] { (byte)0x00, (byte)0x11, (byte)0x22, };
static readonly byte[] BomThenOtherBytes =  new[] { (byte)0xEF, (byte)0xBB, (byte)0xBF, (byte)0x00, (byte)0x11, (byte)0x22, };
static readonly byte[] OtherBytesThenBom =  new[] { (byte)0x00, (byte)0x11, (byte)0x22, (byte)0xEF, (byte)0xBB, (byte)0xBF, };


[TestMethod]
[ExpectedException(typeof(ArgumentNullException))]
public void
Null_stream_throws_ArgumentNullException()
{
    FileExtensions.StartsWithUtf8Bom((Stream)null);
}


[TestMethod]
public void
Bom_by_itself_is_true()
{
    StartsWithUtf8Bom(true, Bom);
}


[TestMethod]
public void
Bom_then_other_bytes_is_true()
{
    StartsWithUtf8Bom(true, BomThenOtherBytes);
}


[TestMethod]
public void
Partial_Bom_is_false()
{
    StartsWithUtf8Bom(false, PartialBom);
}


[TestMethod]
public void
Not_Bom_is_false()
{
    StartsWithUtf8Bom(false, OtherBytes);
}


[TestMethod]
public void
Bom_preceded_by_other_bytes_is_false()
{
    StartsWithUtf8Bom(false, OtherBytesThenBom);
}


void
StartsWithUtf8Bom(bool expected, params byte[] bytes)
{
    var stream = new MemoryStream(bytes);
    Assert.AreEqual(expected, FileExtensions.StartsWithUtf8Bom(stream));
}


}
}
