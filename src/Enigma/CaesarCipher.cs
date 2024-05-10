namespace Enigma;

public class CaesarCipher : SubstitutionCipher
{
    public CaesarCipher(int offset)
        : base(Splice(Alphabet.PlainText, offset))
    { }
    
    private static char[] Splice(string alphabet, int offset) => alphabet.ToCharArray().Rotate(offset);
}