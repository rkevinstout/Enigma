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
    private readonly int _offset = offset.Normalize();

    public int Encode(int i)  => Decode(i.ToChar()).ToInt();

    public char Encode(char c) => (c.ToInt() + _offset).Normalize().ToChar();
    public int Decode(int i) => Decode(i.ToChar()).ToInt();

    public char Decode(char c) => (c.ToInt() + (_offset * -1)).Normalize().ToChar();

    public ICipher Inversion => new CaesarCipher(_offset * -1);
}