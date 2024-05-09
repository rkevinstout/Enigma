namespace Enigma;


public class PlugBoard : ICipher
{
    public string Name { get; } = "PlugBoard";
    
    public readonly SubstitutionCipher Cipher = new();

    public void Add(char from, char to)
    {
        if (from == to) return;

        Cipher.Dictionary[from] = to;
        Cipher.Dictionary[to] = from;
    }

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