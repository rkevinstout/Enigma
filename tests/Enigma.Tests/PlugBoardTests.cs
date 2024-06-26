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

    private static PlugBoard Create(PlugBoard.Pair[] pairs) => new(pairs);

    [Theory]
    [MemberData(nameof(TestPairs))]
    public void ShouldEncode(char from, char to)
    {
        PlugBoard.EncodeChar(from).Should().Be(to);
        PlugBoard.EncodeChar(to).Should().Be(from);
    }

    [Fact]
    public void ShouldBeReciprocal()
    {   
        var dictionary = PlugBoard.CharacterMap.ToDictionary();

        dictionary.Should().AllSatisfy(x => dictionary[x.Value].Should().Be(x.Key));
    }

    public static TheoryData<char, char> TestPairs => Pairs
        .Select(x => new Tuple<char, char>(x.From, x.To))
        .ToTheoryData();
}