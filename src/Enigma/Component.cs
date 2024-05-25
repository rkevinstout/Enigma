namespace Enigma;

public interface IComponent
{
    public CharacterMap CharacterMap { get; }
    
    public CharacterMap InvertedMap { get; }
    public string Name { get; }
    int Encode(int i);
    int Decode(int i);
}