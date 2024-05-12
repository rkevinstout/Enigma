using FluentAssertions;

namespace Enigma.Tests;

public class ReflectorTests
{
    [Theory]
    [ClassData(typeof(TestData))]
    public void ShouldBeSelfReciprocal(ReflectorName name)
    {
        var reflector = Reflector.Create(name);
        
        var dictionary = reflector.Cipher.ToDictionary();

        dictionary.Should().AllSatisfy(x => dictionary[x.Value].Should().Be(x.Key));
    }

    private class TestData : TheoryData<ReflectorName>
    {
        public TestData() =>
            ReflectorFactory.Alphabets
                .Select(x => x.Key)
                .ToList()
                .ForEach(Add);
    }
}