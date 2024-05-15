using FluentAssertions;

namespace Enigma.Tests;

public class RingTests
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

    private class DefaultSettings : TheoryData<RotorName, char[]>
    {
        public DefaultSettings() => RotorConfiguration.Notches
            .ToList()
            .ForEach(x => Add(x.Key, x.Value));
    }
}