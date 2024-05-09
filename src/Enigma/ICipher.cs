namespace Enigma;

public interface ICipher
{
    public string Name { get; }
    char Encode(char c);
    char Encode(int i);
    char Decode(char c);
    char Decode(int i);
    ICipher Inversion { get; }
}