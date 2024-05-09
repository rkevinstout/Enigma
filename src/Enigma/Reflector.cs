namespace Enigma;

public abstract class Reflector : ICipher
{
    public abstract string Name { get; }
    public readonly SubstitutionCipher Cipher;

    private Reflector(SubstitutionCipher cipher) => Cipher = cipher;

    protected Reflector(string alphabet)
        : this(new SubstitutionCipher(alphabet))
    { }

    public char Encode(char c) => Cipher.Encode(c);

    public char Decode(char c) => Cipher.Decode(c);

    public char Encode(int i) => Cipher.Encode(i);

    public char Decode(int i) => Cipher.Decode(i);

    public ICipher Inversion => Cipher.Inversion;

    public override string ToString()
    {
        return Cipher.ToString();
    }
}