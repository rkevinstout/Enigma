using System.Text;
using FluentAssertions;
using Xunit.Abstractions;

namespace Enigma.Tests;

public class RotorTests(ITestOutputHelper output)
{   
    [Fact]
    public void PositionShouldInitializeToZero()
    {
        var rotor = Rotor.Create(RotorName.III);

        rotor.Position.Should().Be(0);
    }

    [Fact]
    public void RingShouldInitializeToZero()
    {
        var defaultRing = Rotor.Create(RotorName.II);

        defaultRing.RingPosition.ToChar().Should().Be('A');
        
        var explicitRing = Rotor.Create(RotorName.II, 'A');

        defaultRing.ToString().Should().Be(explicitRing.ToString());
    }

    [Fact]
    public void RotorShouldRollOver()
    {
        var rotor = Rotor.Create(RotorName.I);

        rotor.Position = 25;
        rotor.Advance();

        rotor.Position.Should().Be(0);
        rotor.CharacterMap.ToString().Should().Be(Alphabet.I);
    }

    [Theory]
    [InlineData('A', 'A', 'E')]
    [InlineData('B', 'A', 'K')]
    public void RingSettingShouldRotateWiring(char ring, char plaintext, char expected)
    {
        var rotor = Rotor.Create(RotorName.I, ring);

        var result = rotor.Encode(plaintext.ToInt()).ToChar();
        output.WriteLine(rotor.ToString());

        result.Should().Be(expected);
    }
}