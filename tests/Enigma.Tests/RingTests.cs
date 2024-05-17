using FluentAssertions;
using Xunit.Abstractions;

namespace Enigma.Tests;

public class RingTests(ITestOutputHelper output)
{
    [Theory]
    [MemberData(nameof(Notches))]
    public void ShouldInitializeDefaults(RotorName name, char[] notches)
    {
        var ring = Ring.Create(name);

        ring.Position.Should().Be('A');
        ring.Notches.Should().BeEquivalentTo(notches);
    }
    
    //[Fact]
    public void UpdatingPositionShouldUpdateNotches()
    {
        var ring = Ring.Create(RotorName.I, 'B');

        ring.Notches.Single().Should().Be('R');
    }
    
    //[Fact]
    public void UpdatingPositionShouldUpdateMultipleNotches()
    {
        var ring = Ring.Create(RotorName.VI, 'B');
        
        char[] expected = ['B', 'O'];

        ring.Notches.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void DumpAlphabet()
    {
        Alphabet.PlainText
            .ToCharArray()
            .Select((c, index) => new ValueTuple<char, int>(c, index))
            .ToList()
            .ForEach(x => output.WriteLine($"{x.Item1} {x.Item2}"));
    }
    
    public static TheoryData<RotorName, char[]> Notches =>  RotorConfiguration.Notches
        .Select(x => new Tuple<RotorName, char[]>(x.Key, x.Value))
        .ToTheoryData();
}