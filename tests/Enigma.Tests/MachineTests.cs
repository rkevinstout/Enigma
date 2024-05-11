using System.Text;
using FluentAssertions;
using Xunit.Abstractions;

namespace Enigma.Tests;

public class MachineTests(ITestOutputHelper output)
{
    private static Machine Build(params RotorName[] rotors)
    {
        var spindle = new Spindle(rotors);

        var reflector = new ReflectorB();
        
        return new Machine(spindle, reflector);
    }

    [Fact]
    public void GShouldEncodeToP()
    {
        // https://www.codesandciphers.org.uk/enigma/example1.htm

        var machine = Build(RotorName.I, RotorName.II, RotorName.III);

        machine.Spindle.Position = "AAZ";
        
        var result = machine.Enter('G');
        
        LogOutput(machine);

        result.Should().Be('P');
        machine.Spindle.Position.Should().Be("AAA");
    }

    [Fact]
    public void AAAAA_BDZGO()
    {
        // https://en.wikipedia.org/wiki/Enigma_rotor_details#Rotor_offset
        const string plainText = "AAAAA";
        const string cipherText = "BDZGO";
        
        var machine = Build(RotorName.I, RotorName.II, RotorName.III);
        //var machine = Build(RotorName.III, RotorName.II, RotorName.I);

        machine.Spindle.Position = "AAA";

        var result = EncodeAndLog(machine, plainText);

        result.Should().Be(cipherText);
    }

    [Fact]
    public void EncryptionIsReciprocal()
    {
        var plainText = "AAAAA";
        
        var machine = Build(RotorName.I, RotorName.II, RotorName.III);

        machine.Spindle.Position = "AAA";

        var cipherText = EncodeAndLog(machine, plainText);
        
        machine.Log.Reset();
        
        machine.Spindle.Position = "AAA";
        
        var result = EncodeAndLog(machine, cipherText);

        result.Should().Be(plainText);
    }

    [Fact]
    public void ShouldCreateCipher()
    {
        var machine = Build(RotorName.I, RotorName.II, RotorName.III);

        var cipher = machine.ToCipher();

        cipher.Dictionary.Keys.Should().OnlyHaveUniqueItems();
        cipher.Dictionary.Values.Should().OnlyHaveUniqueItems();
        cipher.Should().BeSelfReciprocal();
        cipher.ToString().Should().Be("UEJOBTPZWCNSRKDGVMLFAQIYXH");
    }

    private string EncodeAndLog(Machine machine, string input)
    {
        var buffer = new StringBuilder();

        foreach (var c in input.ToCharArray())
        {
            var encoded = machine.Enter(c);

            buffer.Append(encoded);
        }
        
        LogOutput(machine);

        return buffer.ToString();
    }
    
    private void LogOutput(Machine machine)
    {
        foreach (var x in machine.Log.Records)
        {
            output.WriteLine(x.ToString());
        }
    }
}