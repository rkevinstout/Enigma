using FluentAssertions;

namespace Enigma.Tests;

public class PlugBoardTests
{
    [Fact]
    public void PlugBoardsAreSelfReciprocal()
    {
        var pb = new PlugBoard();

        pb.Add('A', 'Z');
        pb.Add('B', 'Y');
        pb.Add('C', 'X');
        pb.Add('D', 'W');
        pb.Add('E', 'V');
        
        var inverted = pb.Inversion;

        pb.ToString().Should().Be(inverted.ToString());
    }
}