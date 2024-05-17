namespace Enigma;

public interface IComponent
{
    public CharacterMap CharacterMap { get; }
    public string Name { get; }
    char Encode(char c);
    char Decode(char c);
}