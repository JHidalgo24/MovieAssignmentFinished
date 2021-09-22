using Microsoft.VisualStudio.TestPlatform.TestHost;
using MovieListAssignment;
using Xunit;

public class testclass{

    [Fact]
    public void PassingDuplicateTest(){
        Assert.True(MovieListAssignment.Program.DuplicateChecker("Toy Story 1"));
    }

    [Theory]
    [InlineData("Toy Story 2")]
    [InlineData("Narnia")]
    [InlineData("Toy Story 5")]
    [InlineData("Hot Rod")]
    public void TheoryTest(string movie){
        Assert.True(MovieListAssignment.Program.DuplicateChecker(movie));
    }
}