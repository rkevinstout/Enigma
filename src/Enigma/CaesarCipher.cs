namespace Enigma;

public class CaesarCipher(int offset) : ICipher
{
    private readonly int _offset = offset.Normalize();

    public int Encode(int i)  => Decode(i.ToChar()).ToInt();

    public char Encode(char c) => (c.ToInt() + _offset).Normalize().ToChar();
    public int Decode(int i) => Decode(i.ToChar()).ToInt();

    public char Decode(char c) => (c.ToInt() + (_offset * -1)).Normalize().ToChar();

    public ICipher Inversion => new CaesarCipher(_offset * -1);
}