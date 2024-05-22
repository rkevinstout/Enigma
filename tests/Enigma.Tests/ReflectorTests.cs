using FluentAssertions;

namespace Enigma.Tests;

public class ReflectorTests
{
    [Theory]
    [MemberData(nameof(ReflectorNames))]
    public void ShouldBeSelfReciprocal(ReflectorName name)
    {
        var reflector = Reflector.Create(name);
        
        var dictionary = reflector.CharacterMap.ToDictionary();

        dictionary.Should().AllSatisfy(x => dictionary[x.Value].Should().Be(x.Key));
    }
    
    public static TheoryData<ReflectorName> ReflectorNames => 
        Configuration.ReflectorSettings.Data.Select(x => x.Key)
            .ToTheoryData();

}