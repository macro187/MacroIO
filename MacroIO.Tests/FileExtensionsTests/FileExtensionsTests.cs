using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacroSystem;


namespace
MacroIO.Tests.FileExtensionsTests
{


[TestClass]
public class
DetectLineEndingsTests
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
Null_throws_ArgumentNullException()
{
    FileExtensions.DetectLineEndings((TextReader)null);
}


[TestMethod]
public void
No_line_endings_returns_null()
{
    DetectLineEndings(null, NoLineEndings);
}


[TestMethod] public void CRLF_only_is_correct() { DetectLineEndings(LineEnding.CRLF, CRLFOnly); }
[TestMethod] public void CR_only_is_correct() { DetectLineEndings(LineEnding.CR, CROnly); }
[TestMethod] public void LF_only_is_correct() { DetectLineEndings(LineEnding.LF, LFOnly); }
[TestMethod] public void CRLF_at_start_is_correct() { DetectLineEndings(LineEnding.CRLF, CRLFAtStart); }
[TestMethod] public void CR_at_start_is_correct() { DetectLineEndings(LineEnding.CR, CRAtStart); }
[TestMethod] public void LF_at_start_is_correct() { DetectLineEndings(LineEnding.LF, LFAtStart); }
[TestMethod] public void CRLF_in_middle_is_correct() { DetectLineEndings(LineEnding.CRLF, CRLFInMiddle); }
[TestMethod] public void CR_in_middle_is_correct() { DetectLineEndings(LineEnding.CR, CRInMiddle); }
[TestMethod] public void LF_in_middle_is_correct() { DetectLineEndings(LineEnding.LF, LFInMiddle); }
[TestMethod] public void CRLF_at_end_is_correct() { DetectLineEndings(LineEnding.CRLF, CRLFAtEnd); }
[TestMethod] public void CR_at_end_is_correct() { DetectLineEndings(LineEnding.CR, CRAtEnd); }
[TestMethod] public void LF_at_end_is_correct() { DetectLineEndings(LineEnding.LF, LFAtEnd); }


void
DetectLineEndings(LineEnding expected, string s)
{
    using (var text = new StringReader(s)) Assert.AreEqual(expected, FileExtensions.DetectLineEndings(text));
}


}
}
