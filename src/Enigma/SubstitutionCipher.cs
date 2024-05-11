using System.Collections.ObjectModel;

namespace Enigma;

public class SubstitutionCipher : ICipher
{
    private readonly SortedList<char, char> _dictionary;
    public ReadOnlyDictionary<char, char> Dictionary => _dictionary.AsReadOnly();

    public ICipher Inversion => _inversion.Value;

    private readonly Lazy<SubstitutionCipher> _inversion;

    public SubstitutionCipher()
        : this(Alphabet.PlainText)
    { }

    public SubstitutionCipher(string alphabet)
        : this(alphabet.ToCharArray())
    { }

    public SubstitutionCipher(char[] array) 
        : this(array
            .Select((c, index) => new ValueTuple<char, char>(index.ToChar(), c))
            .ToDictionary(x => x.Item1, x => x.Item2))
    { }

    public SubstitutionCipher(Dictionary<char, char> dictionary) 
        : this(new SortedList<char, char>(dictionary))
    { }

    public SubstitutionCipher(SortedList<char, char> dictionary)
    {
        _dictionary = dictionary;
        
        _inversion = new Lazy<SubstitutionCipher>(Invert);
    }

    public char Encode(int i) => Dictionary.Values.ElementAt(i % 26);
    public char Encode(char c) => Dictionary[c];
    public char Decode(char c) => Inversion.Encode(c);
    public char Decode(int i) => Inversion.Encode(i % 26);
    private SubstitutionCipher Invert() => new (Dictionary.Invert());
    public override string ToString() => new(Dictionary.Values.ToArray());
}