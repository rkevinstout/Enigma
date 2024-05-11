namespace Enigma;

public interface ICipher
{
    char Encode(char c);
    char Encode(int i);
    char Decode(char c);
    char Decode(int i);
    ICipher Inversion { get; }

}