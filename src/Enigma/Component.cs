namespace Enigma;

public interface IComponent
{
    public ICipher Cipher { get; }
    public string Name { get; }
    char Encode(char c);
    char Decode(char c);
}