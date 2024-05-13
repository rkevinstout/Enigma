namespace Enigma;

/// <summary>
/// a substitution cipher in which each letter in the
/// plaintext is replaced by a letter some fixed number
/// of positions down the alphabet.
/// </summary>
/// <param name="offset">The number of poitions to shift</param>
/// <seealso cref="https://en.wikipedia.org/wiki/Caesar_cipher"/>
public class CaesarSubstitutionCipher(int offset) 
    : SubstitutionCipher(
        Alphabet.PlainText
            .ToCharArray()
            .Rotate(offset)
        );

public class CaesarCipher(int offset) : ICipher
{
    private readonly int _offset = Normalize(offset);

    private static int Normalize(int input)
    {
        if (input == 0) return input;
        
        var abs = Math.Abs(input) % 26;

        var count = input > 0 ? abs : 26 - abs;

        return count;
    }
    
    public char Encode(char c) => Normalize(c.ToInt() + _offset).ToChar();

    public char Decode(char c) => Normalize(c.ToInt() + (_offset * -1)).ToChar();

    public ICipher Inversion => new CaesarCipher(_offset * -1);
}