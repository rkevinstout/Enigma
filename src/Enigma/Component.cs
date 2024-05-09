namespace Enigma;

public interface IComponent
{
    public SubstitutionCipher Cipher { get; }
    public string Name { get; }
}