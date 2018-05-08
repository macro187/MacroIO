using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace
MacroIO.Tests
{


[TestClass]
public class
PathExtensionsTests
{


[TestMethod]
[ExpectedException(typeof(ArgumentNullException))]
public void
Split_Null_Throws_ArgumentNullException()
{
    PathExtensions.Split(null);
}


[TestMethod]
public void
Split_Empty_Yields_Empty_Array()
{
    Assert.IsTrue(
        PathExtensions.Split("")
            .SequenceEqual(new string[0]));
}


[TestMethod]
public void
Split_Single_Component_Yields_Single_Item_Array()
{
    Assert.IsTrue(
        PathExtensions.Split("a")
            .SequenceEqual(new[]{ "a" }));
}


[TestMethod]
public void
Split_Relative_Path_Yields_Correct_Components()
{
    Assert.IsTrue(
        PathExtensions.Split("a/b/c")
            .SequenceEqual(new[]{ "a", "b", "c" }));
}


[TestMethod]
public void
Split_Path_With_Mixed_Separators_Yields_Correct_Components()
{
    Assert.IsTrue(
        PathExtensions.Split("a/b\\c")
            .SequenceEqual(new[]{ "a", "b", "c" }));
}


[TestMethod]
public void
Split_Discards_Components_Before_Initial_Separators()
{
    Assert.IsTrue(
        PathExtensions.Split("/a/b")
            .SequenceEqual(new[]{ "a", "b" }));
}


[TestMethod]
public void
Split_Discards_Components_After_Final_Separators()
{
    Assert.IsTrue(
        PathExtensions.Split("a/b/")
            .SequenceEqual(new[]{ "a", "b" }));
}


[TestMethod]
public void
Split_Discards_Components_Between_Consecutive_Separators()
{
    Assert.IsTrue(
        PathExtensions.Split("a//b")
            .SequenceEqual(new[]{ "a", "b" }));
}


[TestMethod]
[ExpectedException(typeof(ArgumentNullException))]
public void
IsDescendantOf_Null_Path_Throws_ArgumentNullException()
{
    PathExtensions.IsDescendantOf(null, "a");
}


[TestMethod]
[ExpectedException(typeof(ArgumentNullException))]
public void
IsDescendantOf_Null_PossibleAncestorPath_Throws_ArgumentNullException()
{
    PathExtensions.IsDescendantOf("a", null);
}


[TestMethod]
public void
IsDescendantOf_Same_Is_False()
{
    Assert.IsFalse(PathExtensions.IsDescendantOf("a/b", "a/b"));
}


[TestMethod]
public void
IsDescendantOf_Longer_Is_False()
{
    Assert.IsFalse(PathExtensions.IsDescendantOf("a/b", "a/b/c"));
}


[TestMethod]
public void
IsDescendantOf_Parent_Is_True()
{
    Assert.IsTrue(PathExtensions.IsDescendantOf("a/b/c", "a/b"));
}


[TestMethod]
public void
IsDescendantOf_Ancestor_Is_True()
{
    Assert.IsTrue(PathExtensions.IsDescendantOf("a/b/c/d", "a/b"));
}


[TestMethod]
public void
IsDescendantOf_Is_Case_Sensitive()
{
    Assert.IsFalse(PathExtensions.IsDescendantOf("a/b/c", "A/B"));
}


[TestMethod]
public void
IsDescendantOf_Ignores_Absoluteness()
{
    Assert.IsTrue(PathExtensions.IsDescendantOf("/a/b/c", "a/b"));
}


[TestMethod]
[ExpectedException(typeof(ArgumentNullException))]
public void
GetPathFromAncestor_Null_Path_Throws_ArgumentNullException()
{
    PathExtensions.GetPathFromAncestor(null, "a");
}


[TestMethod]
[ExpectedException(typeof(ArgumentNullException))]
public void
GetPathFromAncestor_Null_AncestorPath_Throws_ArgumentNullException()
{
    PathExtensions.GetPathFromAncestor("a", null);
}


[TestMethod]
[ExpectedException(typeof(ArgumentException))]
public void
GetPathFromAncestor_That_Is_Not_Ancestor_Throws_ArgumentException()
{
    PathExtensions.GetPathFromAncestor("a/b/c", "not/ancestor");
}


[TestMethod]
public void
GetPathFromAncestor_Yields_Correct_Path()
{
    Assert.AreEqual(
        Path.Combine("b", "c"),
        PathExtensions.GetPathFromAncestor("a/b/c", "a"));
}


}
}
