using FluentAssertions;

namespace Enigma.Tests;

public class PlugBoardTests
{
    private static readonly PlugBoard.Pair[] Pairs = [
            new('A', 'Z'),
            new('B', 'Y'),
            new('C', 'X'),
            new('D', 'W'),
            new('E', 'V')
        ];
    private static readonly PlugBoard PlugBoard = Create(Pairs);

    private static PlugBoard Create(PlugBoard.Pair[] pairs)
    {
        var pb = new PlugBoard();
        pb.Add(pairs);

        return pb;
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void ShouldEncode(char from, char to)
    {
        var result = PlugBoard.Cipher.Encode(from);

        result.Should().Be(to);
    }
    
    [Fact]
    public void PlugBoardsAreSelfReciprocal()
    {
        var inverted = PlugBoard.Inversion;

        inverted.ToString().Should().Be(PlugBoard.ToString());
    }

    public class TestData : TheoryData<char, char>
    {
        public TestData()
        {
            foreach (var pair in Pairs)
            {
                Add(pair.From, pair.To);
            }
        }
    }
}