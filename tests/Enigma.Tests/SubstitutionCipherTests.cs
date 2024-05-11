using FluentAssertions;
using Xunit.Abstractions;

namespace Enigma.Tests;

public class SubstitutionCipherTests(ITestOutputHelper output)
{
    [Fact]
    public void CipherShouldBeReciprocal()
    {
        var cipher = new SubstitutionCipher(Alphabet.IV);

        cipher.Should().BeSelfReciprocal();
    }

    [Fact]
    public void RotateTests()
    {
        var cipher = new SubstitutionCipher(Alphabet.I);
        
        output.WriteLine($"{0,3} {cipher}");

        for (var i = 1; i < 55; i++)
        {
            cipher =  cipher.Rotate(i);
            
            output.WriteLine($"{i,3} {cipher}");
        }
    }
}