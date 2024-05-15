namespace Enigma;

public interface ICipher
{
    int Encode(int i);
    char Encode(char c);
    int Decode(int i);
    char Decode(char c);
    ICipher Inversion { get; }

}