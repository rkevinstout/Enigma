namespace Enigma;


public class PlugBoard : IComponent
{
    public string Name { get; } = "PlugBoard";
    
    public SubstitutionCipher Cipher { get; } = new();

    public void Add(char from, char to)
    {
        if (from == to) return;

        Cipher.Dictionary[from] = to;
        Cipher.Dictionary[to] = from;
    }

    public ICipher Inversion => Cipher.Inversion;

    public override string ToString()
    {
        return Cipher.ToString();
    }
}