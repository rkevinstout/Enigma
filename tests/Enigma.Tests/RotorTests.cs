using FluentAssertions;
using Xunit.Abstractions;

namespace Enigma.Tests;

public class RotorTests(ITestOutputHelper output)
{   
    [Fact]
    public void Test()
    {
        var rotor = RotorFactory.Create(RotorName.I);
        
        output.WriteLine("%");
        output.WriteLine(rotor.Dump());

        for (var i = 0; i < 55; i++)
        {
            rotor.Advance();
            
            output.WriteLine(rotor.Dump());
        }
    }

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
    public void Test2()
    {
        var rotor = RotorFactory.Create(RotorName.I);

        rotor.Position = 1;

        var input = 'A';

        var result = rotor.Cipher.Encode(input);

        var expected = 'K';

        result.Should().Be(expected);
        
        output.WriteLine(Alphabet.PlainText);
        output.WriteLine(rotor.ToString());

        var shift = rotor.Shift();
        
        output.WriteLine(shift.ToString());

    }
}