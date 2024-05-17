using System.Text;
using FluentAssertions;
using Xunit.Abstractions;

namespace Enigma.Tests;

public class MachineTests(ITestOutputHelper output)
{
    private static Machine Build(string ringSettings = "AAA", params RotorName[] rotors )
    {
        var rings = ringSettings.AsSpan();
        var config = new Machine.Configuration();
        
        for (var i = 0; i < rotors.Length; i++)
        {
            config.AddRotor(rotors[i], rings[i]);
        }
        
        var machine = config.Create();
        
        machine.Debug = true;
        
        return machine;
    }

    [Fact]
    public void GShouldEncodeToP()
    {
        // https://www.codesandciphers.org.uk/enigma/example1.htm

        var machine = Build("AAA",RotorName.I, RotorName.II, RotorName.III);

        machine.Position = "AAA";
        
        var result = machine.Encode('G');
        
        LogOutput(machine);

        result.Should().Be('P');
        machine.Position.Should().Be("AAA");
    }

    [Theory]
    [InlineData("AAA", "AAAAA", "BDZGO")]
    [InlineData("BBB", "AAAAA", "PGQPW")]
    public void ProducesPredictableCipherText(string position, string plainText, string cipherText)
    {
        // https://en.wikipedia.org/wiki/Enigma_rotor_details#Rotor_offset
        
        var machine = Build("AAA", RotorName.I, RotorName.II, RotorName.III);

        machine.Position = position;

        var result = EncodeAndLog(machine, plainText);

        result.Should().Be(cipherText);
    }
    [Theory]
    [InlineData("AAA", "AAA", "AAAAA", "BDZGO")]
    [InlineData("AAA", "BBB", "AAAAA", "EWTYX")]
    public void RingSettingsShouldEffectOffset(string position, string ringSettings, string plainText, string cipherText)
    {
        // https://en.wikipedia.org/wiki/Enigma_rotor_details#Ring_setting

        var rings = ringSettings.ToCharArray();

        var config = new Machine.Configuration();
        
        config.AddRotor(RotorName.I, rings[0]);
        config.AddRotor(RotorName.II, rings[1]);
        config.AddRotor(RotorName.III, rings[2]);

        var machine = config.Create();

        machine.Position = position;

        var result = EncodeAndLog(machine, plainText);

        result.Should().Be(cipherText);
    }

    [Theory]
    [ClassData(typeof(TestConfigurations))]
    public void EncryptionIsSymetric (string ringSettings, string position, string plainText)
    {
        var machine = Build(ringSettings, RotorName.I, RotorName.II, RotorName.III);

        machine.Position = position;

        var cipherText = EncodeAndLog(machine, plainText);
        
        machine.Log.Reset();

        machine.Position = position;
        
        var result = EncodeAndLog(machine, cipherText);

        result.Should().Be(plainText);
    }

    [Fact]
    public void ShouldCreateCipher()
    {
        var machine = Build("AAA", RotorName.I, RotorName.II, RotorName.III);

        var cipher = machine.ToCipher();
        
        var dictionary = cipher.ToDictionary();

        dictionary.Keys.Should().OnlyHaveUniqueItems();
        dictionary.Values.Should().OnlyHaveUniqueItems();
        cipher.ToString().Should().Be("UEJOBTPZWCNSRKDGVMLFAQIYXH");
        
        dictionary.Should().AllSatisfy(x => 
            dictionary[x.Value].Should().Be(x.Key)
            );
    }


    private string EncodeAndLog(Machine machine, string input)
    {
        var buffer = new StringBuilder();

        foreach (var c in input.ToCharArray())
        {
            var encoded = machine.Enter(c);

            buffer.Append(encoded);
            
            LogOutput(machine);
            
            machine.Log.Reset();
            
            output.WriteLine(new('=', 50));
        }
        return buffer.ToString();
    }
    
    private void LogOutput(Machine machine) => machine.Log.Records
        .ForEach(x => output.WriteLine(x.ToString()));

    public class TestConfigurations : TheoryData<string, string, string>
    {
        public TestConfigurations()
        {
            var text = GenerateText(256);
            Add("AAA", "AAA", text);
            Add("BBB", "BBB", text);
            Add("ABC", "DEF", text);
            Add("XYZ", "YHF", text);
        }
    }
    
    private static string GenerateText(int length) => Extensions.GenerateText(length);
}