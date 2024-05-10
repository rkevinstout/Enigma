using Xunit.Abstractions;

namespace Enigma.Tests;

public class RotationTests(ITestOutputHelper output)
{
    [Fact]
    public void RotateLeft()
    {
        var chars = Alphabet.I.ToCharArray();
        
        for (int i = 0; i < chars.Length * 2; i++)
        {
            var rotated = chars.Rotate(i);
        
            output.WriteLine($"{i, 5} {new string(rotated)}");
        }
    }
    
    [Fact]
    public void RotateRight()
    {
        var chars = Alphabet.I.ToCharArray();
        
        for (int i = 0; i < chars.Length * 2; i++)
        {
            var rotated = chars.Rotate(i * -1);
        
            output.WriteLine($"{i, 5} {new string(rotated)}");
        }
    }
}