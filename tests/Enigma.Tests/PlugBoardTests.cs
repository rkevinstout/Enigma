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
    public void ShouldBeReciprocal()
    {   
        var dictionary = PlugBoard.Cipher.ToDictionary();

        dictionary.Should().AllSatisfy(x => dictionary[x.Value].Should().Be(x.Key));
    }

    private class TestData : TheoryData<char, char>
    {
        public TestData() => Pairs
            .ToList()
            .ForEach(p => Add(p.From, p.To));
    }
}