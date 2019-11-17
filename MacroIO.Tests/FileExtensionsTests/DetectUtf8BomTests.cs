using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace
MacroIO.Tests.FileExtensionsTests
{


[TestClass]
public class
DetectUtf8BomTests
{


static readonly byte[] Bom =                new[] { (byte)0xEF, (byte)0xBB, (byte)0xBF, };
static readonly byte[] PartialBom =         new[] { (byte)0xEF, (byte)0xBB, };
static readonly byte[] OtherBytes =         new[] { (byte)0x00, (byte)0x11, (byte)0x22, };
static readonly byte[] BomThenOtherBytes =  new[] { (byte)0xEF, (byte)0xBB, (byte)0xBF, (byte)0x00, (byte)0x11, (byte)0x22, };
static readonly byte[] OtherBytesThenBom =  new[] { (byte)0x00, (byte)0x11, (byte)0x22, (byte)0xEF, (byte)0xBB, (byte)0xBF, };
static readonly byte[] Empty =              new byte[0];


[TestMethod]
[ExpectedException(typeof(ArgumentNullException))]
public void
Null_stream_throws_ArgumentNullException()
{
    FileExtensions.DetectUtf8Bom((Stream)null);
}


[TestMethod]
public void
Bom_by_itself_is_true()
{
    DetectUtf8Bom(true, Bom);
}


[TestMethod]
public void
Bom_then_other_bytes_is_true()
{
    DetectUtf8Bom(true, BomThenOtherBytes);
}


[TestMethod]
public void
Partial_Bom_is_false()
{
    DetectUtf8Bom(false, PartialBom);
}


[TestMethod]
public void
Not_Bom_is_false()
{
    DetectUtf8Bom(false, OtherBytes);
}


[TestMethod]
public void
Bom_preceded_by_other_bytes_is_false()
{
    DetectUtf8Bom(false, OtherBytesThenBom);
}


[TestMethod]
public void
Empty_is_null()
{
    DetectUtf8Bom(null, Empty);
}


void
DetectUtf8Bom(bool? expected, params byte[] bytes)
{
    var stream = new MemoryStream(bytes);
    Assert.AreEqual(expected, FileExtensions.DetectUtf8Bom(stream));
}


}
}
