using System.Text;
using FluentAssertions;
using Xunit.Abstractions;

namespace Enigma.Tests;

public class MachineTests(ITestOutputHelper output)
{
    private static Machine Build(string ringSettings = "AAA", params RotorName[] rotors )
    {
        var rings = ringSettings.ToCharArray();
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

        cipher.Dictionary.Keys.Should().OnlyHaveUniqueItems();
        cipher.Dictionary.Values.Should().OnlyHaveUniqueItems();
        cipher.ToString().Should().Be("UEJOBTPZWCNSRKDGVMLFAQIYXH");
        
        cipher.Dictionary.Should().AllSatisfy(x => 
            cipher.Dictionary[x.Value].Should().Be(x.Key)
            );
    }

    [Fact]
    public void TonySaleExample()
    {
        // https://www.codesandciphers.org.uk/enigma/emachines/enigmad.htm
        var config = new Machine.Configuration();
        
        config.AddRotor(RotorName.IV, 'G');
        config.AddRotor(RotorName.II, 'M');
        config.AddRotor(RotorName.V, 'Y');
        config.ReflectorName = ReflectorName.RefB;
        
        // DN GR IS KC QX TM PV HY FW BJ
        config.AddPairs(
            new('D','N'), new('G','R'), new('I','S'), new('K','C'), new('Q','X'), 
            new('T','M'), new('P','V'), new('H','Y'), new('F','W'), new('B','J')
            );
        
        var machine = config.Create();

        machine.Position = "DHO";

        var key = machine.Encode("GXS");
        
        key.Should().Be("RLP");

        machine.Position = key;

        const string cipherText = "NQVLT YQFSE WWGJZ GQHVS EIXIM YKCNW IEBMB ATPPZ TDVCU PKAY";
        
        var result = machine.Encode(cipherText);
        
        output.WriteLine(result);
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
    
    private void LogOutput(Machine machine)
    {
        foreach (var x in machine.Log.Records)
        {
            output.WriteLine(x.ToString());
        }
    }
    
    

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