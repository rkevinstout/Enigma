using FluentAssertions;
using Xunit.Abstractions;

namespace Enigma.Tests;

public class RotorTests(ITestOutputHelper output)
{   
    [Fact]
    public void PositionShouldInitializeToZero()
    {
        var rotor = RotorFactory.Create(RotorName.III);

        rotor.Position.Should().Be(0);
    }

    [Fact]
    public void RotorShouldRollOver()
    {
        var rotor = RotorFactory.Create(RotorName.I);

        rotor.Position = 25;
        rotor.Advance();

        rotor.Position.Should().Be(0);
        rotor.Cipher.ToString().Should().Be(Alphabet.I);
    }
}