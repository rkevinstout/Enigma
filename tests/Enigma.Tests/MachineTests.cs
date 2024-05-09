using FluentAssertions;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Enigma.Tests;

public class MachineTests
{
    private readonly Machine _machine = Build();

    private readonly ITestOutputHelper _output;

    public MachineTests(ITestOutputHelper output)
    {
        _output = output;
    } 
    private static Machine Build()
    {
        var spindle = new Spindle(
            position: "AAZ",
            RotorFactory.Create(RotorName.III),
            RotorFactory.Create(RotorName.II),
            RotorFactory.Create(RotorName.I)
        );

        var reflector = new ReflectorB();
        
        return new Machine(spindle, reflector);
    }

    [Fact]
    public void GShouldEncodeToP()
    {
        // https://www.codesandciphers.org.uk/enigma/example1.htm
        
        var result = _machine.Encode('G');
        
        LogOutput(_machine);

        result.Should().Be('P');
        _machine.Spindle.Position.Should().Be("AAA");
    }

    private void LogOutput(Machine machine)
    {
        machine.Log.ForEach(x => _output.WriteLine(x.ToString()));
    }
}