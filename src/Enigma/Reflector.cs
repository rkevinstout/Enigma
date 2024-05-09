namespace Enigma;

public abstract class Reflector : IComponent
{
    public abstract string Name { get; }
    public SubstitutionCipher Cipher { get; }

    private Reflector(SubstitutionCipher cipher) => Cipher = cipher;

    protected Reflector(string alphabet)
        : this(new SubstitutionCipher(alphabet))
    { }

    public ICipher Inversion => Cipher.Inversion;

    public override string ToString()
    {
        return Cipher.ToString();
    }
}