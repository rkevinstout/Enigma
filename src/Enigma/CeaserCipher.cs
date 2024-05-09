namespace Enigma;

class CaesarCipher : SubstitutionCipher
{
    public new string Name { get; }

    public CaesarCipher(int offset)
        : base(Splice(Alphabet.PlainText, offset))
    {
        Name = $"Ceaser+{offset}";
    }
    
    private static char[] Splice(string alphabet, int offset)
    {
        var chars = alphabet.ToCharArray();

        offset = Math.Abs(offset % 26);
            
        return chars.Skip(offset)
            .Concat(chars.Take(offset))
            .ToArray();
    }
}