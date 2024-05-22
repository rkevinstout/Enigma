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
        var inverse = map.Invert();
        
        output.WriteLine(Alphabet.PlainText);
        output.WriteLine(map.ToString());
        output.WriteLine(inverse.ToString());
        output.WriteLine(Alphabet.PlainText);

        map.Encode('A').Should().Be('E');
        inverse.Encode('E').Should().Be('A');
    }
    
    [Theory]
    [MemberData(nameof(Alphabets))]
    public void MapIsSymetric(string alphabet)
    {
        var map = new CharacterMap(alphabet);
        var inverse = map.Invert();
        
        for (var i = 0; i < alphabet.Length; i++)
        {
            var key = i.ToChar();
            var result = map.Encode(key);
            
            inverse.Encode(result).Should().Be(key);
        }
    }

    [Fact]
    public void IntLookup()
    {
        var map = new CharacterMap(Alphabet.I);
        var inverse = map.Invert();
        
        output.WriteLine(Alphabet.PlainText);
        output.WriteLine(map.ToString());

        map.Encode(0).ToChar().Should().Be('E');
        inverse.Encode(4).ToChar().Should().Be('A');
    }

    [Theory]
    [MemberData(nameof(Rings))]
    public void OffsetTests(char ring)
    {
        var map = new CharacterMap(Alphabet.I);
        var buffer = new StringBuilder();

        for (var i = 0; i < Alphabet.I.Length; i++)
        {
            var shift = ring.ToInt();
            var key = (shift + i).Normalize();
            var result = map.Encode(key).ToChar();
            buffer.Append(result);
        }
        output.WriteLine(buffer.ToString());
    }
    
    [Fact]
    public void ParameterlessCtor()
    {
        var map = new CharacterMap();
        
        var chars = new string(map.Encodings);
        
        chars.Should().BeEquivalentTo(Alphabet.PlainText);
    }

    public static TheoryData<char> Rings => Alphabet.PlainText
        .ToCharArray()
        .ToTheoryData();
    
    public static TheoryData<string> Alphabets => Configuration.RotorSettings.Data
        .Values
        .Select(x => x.Wiring)
        .ToTheoryData();

}