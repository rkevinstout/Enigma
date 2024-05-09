using FluentAssertions;

namespace Enigma.Tests;

public class SubstitutionCipherTests
{
    [Fact]
    public void CipherShouldBeReciprocal()
    {
        var cipher = new SubstitutionCipher(Alphabet.IV);

        var doubleInversion = cipher.Inversion.Inversion;

        doubleInversion.ToString().Should().Be(cipher.ToString());
    }
}