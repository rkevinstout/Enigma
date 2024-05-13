using System.Runtime.InteropServices;
using FluentAssertions;

namespace Enigma.Tests;

public class CaesarCipherTests()
{
    [Fact]
    public void WikipediaExampleShouldWork()
    {
        // https://en.wikipedia.org/wiki/Caesar_cipher#Example
        
        var cipher = new CaesarSubstitutionCipher(-3);

        const string plainText = "THE QUICK BROWN FOX JUMPS OVER THE LAZY DOG";

        var result = cipher.Encode(plainText);
        
        const string cipherText = "QEB NRFZH YOLTK CLU GRJMP LSBO QEB IXWV ALD";

        result.Should().Be(cipherText);
    }

    [Fact]
    public void ShouldShiftRight()
    {
        var cipher = new CaesarSubstitutionCipher(-1);

        var result = cipher.Encode('B');

        result.Should().Be('A');
    }
    
    [Fact]
    public void ShouldShiftLeft()
    {
        var cipher = new CaesarSubstitutionCipher(1);

        var result = cipher.Encode('B');

        result.Should().Be('C');
    }

    [Fact]
    public void InversionTests()
    {
        var left = new CaesarSubstitutionCipher(1);
        var right = new CaesarSubstitutionCipher(-1);

        var leftInversion = left.Inversion;

        leftInversion.ToString().Should().Be(right.ToString());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(13)]
    [InlineData(25)]
    [InlineData(35)]
    [InlineData(95)]
    public void AlgoShouldEqualDictionary(int shift)
    {
        var left = new CaesarSubstitutionCipher(shift);
        var algo = new CaesarCipher(shift);

        left.Dictionary.Should().Equal(algo.ToDictionary());

        left.Inversion.ToDictionary().Should().Equal(algo.Inversion.ToDictionary());
    }
}