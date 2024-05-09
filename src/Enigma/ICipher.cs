namespace Enigma;

public interface ICipher
{
    public string Name { get; }
    char Encode(char c);
    char Decode(char c);
    char Encode(int i);
    char Decode(int i);
    ICipher Inversion { get; }
}