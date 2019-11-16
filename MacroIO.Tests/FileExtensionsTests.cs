using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacroSystem;


namespace
MacroIO.Tests
{


[TestClass]
public class
FileExtensionsTests
{


const string NoLineEndings =    "...";
const string CRLFOnly =         "\x0D\x0A";
const string CROnly =           "\x0D";
const string LFOnly =           "\x0A";
const string CRLFAtStart =      "\x0D\x0A...";
const string CRAtStart =        "\x0D...";
const string LFAtStart =        "\x0A...";
const string CRLFInMiddle =     "...\x0D\x0A...";
const string CRInMiddle =       "...\x0D...";
const string LFInMiddle =       "...\x0A...";
const string CRLFAtEnd =        "...\x0D\x0A";
const string CRAtEnd =          "...\x0D";
const string LFAtEnd =          "...\x0A";


[TestMethod]
[ExpectedException(typeof(ArgumentNullException))]
public void
DetectLineEndings_null_throws_ArgumentNullException()
{
    FileExtensions.DetectLineEndings((TextReader)null);
}


[TestMethod]
public void
DetectLineEndings_no_line_endings_returns_null()
{
    DetectLineEndings(null, NoLineEndings);
}


[TestMethod] public void DetectLineEndings_CRLF_only() { DetectLineEndings(LineEnding.CRLF, CRLFOnly); }
[TestMethod] public void DetectLineEndings_CR_only() { DetectLineEndings(LineEnding.CR, CROnly); }
[TestMethod] public void DetectLineEndings_LF_only() { DetectLineEndings(LineEnding.LF, LFOnly); }
[TestMethod] public void DetectLineEndings_CRLF_at_start() { DetectLineEndings(LineEnding.CRLF, CRLFAtStart); }
[TestMethod] public void DetectLineEndings_CR_at_start() { DetectLineEndings(LineEnding.CR, CRAtStart); }
[TestMethod] public void DetectLineEndings_LF_at_start() { DetectLineEndings(LineEnding.LF, LFAtStart); }
[TestMethod] public void DetectLineEndings_CRLF_in_middle() { DetectLineEndings(LineEnding.CRLF, CRLFInMiddle); }
[TestMethod] public void DetectLineEndings_CR_in_middle() { DetectLineEndings(LineEnding.CR, CRInMiddle); }
[TestMethod] public void DetectLineEndings_LF_in_middle() { DetectLineEndings(LineEnding.LF, LFInMiddle); }
[TestMethod] public void DetectLineEndings_CRLF_at_end() { DetectLineEndings(LineEnding.CRLF, CRLFAtEnd); }
[TestMethod] public void DetectLineEndings_CR_at_end() { DetectLineEndings(LineEnding.CR, CRAtEnd); }
[TestMethod] public void DetectLineEndings_LF_at_end() { DetectLineEndings(LineEnding.LF, LFAtEnd); }


void
DetectLineEndings(LineEnding expected, string s)
{
    using (var text = new StringReader(s)) Assert.AreEqual(expected, FileExtensions.DetectLineEndings(text));
}


}
}
