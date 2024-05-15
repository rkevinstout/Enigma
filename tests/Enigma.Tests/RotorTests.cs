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

        defaultRing.Ring.Position.Should().Be('A');
        
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
        rotor.Cipher.ToString().Should().Be(Alphabet.I);
    }

    [Theory]
    [InlineData('A', 'A', 'E')]
    [InlineData('B', 'A', 'K')]
    public void RingSettingShouldRotateWiring(char ring, char plaintext, char expected)
    {
        var rotor = Rotor.Create(RotorName.I, ring);

        var result = rotor.Cipher.Encode(plaintext);

        result.Should().Be(expected);
    }

    [Fact]
    public void MergeWithShift()
    {
        var rotor = Rotor.Create(RotorName.I);

        rotor.Position = 1;

        var shift = rotor.Shift;

        var list = new List<ICipher> { rotor.Cipher, shift };

        var buffer = new StringBuilder();

        foreach (var c in Alphabet.PlainText.ToCharArray())
        {
            var temp = list.Aggregate(c, (current, cipher) => cipher.Encode(current));
            buffer.Append(temp);
        }

        var result = new SubstitutionCipher(buffer.ToString());
        
        output.WriteLine(rotor.ToString());
        output.WriteLine(shift.ToString());
        output.WriteLine(result.ToString());
    }
}