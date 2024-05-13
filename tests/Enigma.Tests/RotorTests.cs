using System.Text;
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

    [Fact]
    public void MergeWithShift()
    {
        var rotor = RotorFactory.Create(RotorName.I);

        rotor.Position = 1;

        var shift = rotor.Shift();

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