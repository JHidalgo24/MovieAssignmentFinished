using Microsoft.VisualStudio.TestPlatform.TestHost;
using MovieAssignment;
using Xunit;

public class testclass
{

    //should pass as Toy Story 1 is in the file
    [Fact]
    public void PassingDuplicateTest()
    {
        Assert.True(MovieAssignment.Program.DuplicateChecker("Toy Story"));
    }


    //only Toy Story 5 should fail
    [Theory]
    [InlineData("Toy Story 2")]
    [InlineData("Narnia")]
    [InlineData("Toy Story 5")]
    [InlineData("Hot Rod")]
    public void TheoryTest(string movie)
    {
        Assert.True(MovieAssignment.Program.DuplicateChecker(movie));
    }
}