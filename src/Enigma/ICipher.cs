namespace Enigma;

public interface ICipher
{
    char Encode(char c);
    char Decode(char c);
    ICipher Inversion { get; }

}