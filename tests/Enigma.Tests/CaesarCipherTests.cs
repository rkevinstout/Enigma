using FluentAssertions;

namespace Enigma.Tests;

public class CaesarCipherTests
{
    [Fact]
    public void WikipediaExampleShouldWork()
    {
        // https://en.wikipedia.org/wiki/Caesar_cipher#Example
        
        var cipher = new CaesarCipher(-3);

        const string plainText = "THE QUICK BROWN FOX JUMPS OVER THE LAZY DOG";

        var result = cipher.Encode(plainText);
        
        const string cipherText = "QEB NRFZH YOLTK CLU GRJMP LSBO QEB IXWV ALD";

        result.Should().Be(cipherText);
    }

    [Fact]
    public void ShouldShiftRight()
    {
        var cipher = new CaesarCipher(-1);

        var result = cipher.Encode('B');

        result.Should().Be('A');
    }
    
    [Fact]
    public void ShouldShiftLeft()
    {
        var cipher = new CaesarCipher(1);

        var result = cipher.Encode('B');

        result.Should().Be('C');
    }
}