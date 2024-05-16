using System.Text;
using FluentAssertions;
using Xunit.Abstractions;

namespace Enigma.Tests;

public class CharacterMapTests(ITestOutputHelper output)
{
    [Fact]
    public void CharLookup()
    {
        var map = new CharacterMap(Alphabet.I);
        var inverse = map.Inversion;
        
        output.WriteLine(Alphabet.PlainText);
        output.WriteLine(map.ToString());
        output.WriteLine(inverse.ToString());
        output.WriteLine(Alphabet.PlainText);

        map.Encode('A').Should().Be('E');
        map.Decode('E').Should().Be('A');
        
    }

    [Fact]
    public void IntLookup()
    {
        var map = new CharacterMap(Alphabet.I);
        
        output.WriteLine(Alphabet.PlainText);
        output.WriteLine(map.ToString());

        map.Encode(0).ToChar().Should().Be('E');
        
        map.Decode(4).ToChar().Should().Be('A');
    }

    [Theory]
    [MemberData(nameof(Rings))]
    public void OffsetTests(char ring)
    {
        var ringSettings = Ring.Create(RotorName.I, ring);
        var map = new CharacterMap(Alphabet.I);
        var buffer = new StringBuilder();

        for (var i = 0; i < Alphabet.I.Length; i++)
        {
            var shift = ringSettings.Position.ToInt();
            var key = (shift + i).Normalize();
            var result = map.Encode(key).ToChar();
            buffer.Append(result);
        }
        output.WriteLine(buffer.ToString());
    }


    [Fact]
    public void WTF()
    {
        var map = new CharacterMap(Alphabet.I);
        var cipher = new SubstitutionCipher(Alphabet.I);
        
        output.WriteLine(map.ToString());
        output.WriteLine(cipher.ToString());
        output.WriteLine(String.Empty);
        output.WriteLine(map.Inversion.ToString());
        output.WriteLine(cipher.Inversion.ToString());
        
    }

    public static TheoryData<char> Rings => Alphabet.PlainText
        .ToCharArray()
        .ToTheoryData();

}