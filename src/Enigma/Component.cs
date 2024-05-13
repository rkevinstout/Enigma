namespace Enigma;

public interface IComponent
{
    public ICipher Cipher { get; }
    public string Name { get; }
}