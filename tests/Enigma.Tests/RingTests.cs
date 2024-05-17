using FluentAssertions;
using Xunit.Abstractions;

namespace Enigma.Tests;

public class RingTests(ITestOutputHelper output)
{
    [Theory]
    [ClassData(typeof(DefaultSettings))]
    public void ShouldInitializeDefaults(RotorName name, char[] notches)
    {
        var ring = Ring.Create(name);

        ring.Position.Should().Be('A');
        ring.Notches.Should().BeEquivalentTo(notches);
    }
    
    [Fact]
    public void UpdatingPositionShouldUpdateNotches()
    {
        var ring = Ring.Create(RotorName.I, 'B');

        ring.Notches.Single().Should().Be('S');
    }
    
    [Fact]
    public void UpdatingPositionShouldUpdateMultipleNotches()
    {
        var ring = Ring.Create(RotorName.VI, 'B');

        ring.Notches.Should().BeEquivalentTo(new[] { 'B', 'O' });
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

    private class DefaultSettings : TheoryData<RotorName, char[]>
    {
        public DefaultSettings() => RotorConfiguration.Notches
            .ToList()
            .ForEach(x => Add(x.Key, x.Value));
    }
}