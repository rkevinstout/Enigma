namespace Enigma;

// ReSharper disable InconsistentNaming
public static class Alphabet
{
    public const string PlainText = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const string I   = "EKMFLGDQVZNTOWYHXUSPAIBRCJ";
    public const string II  = "AJDKSIRUXBLHWTMCQGZNPYFVOE";
    public const string III = "BDFHJLCPRTXVZNYEIWGAKMUSQO";
    public const string IV  = "ESOVPZJAYQUIRHXLNFTGKDCMWB";
    public const string V   = "VZBRGITYUPSDNHLXAWMJQOFECK";
    public const string VI  = "JPGVOUMFYQBENHZRDKASXLICTW";
    public const string VII = "NZJHGRCXMYSWBOUFAIVLPEKQDT";
    public const string VIII = "FKQHTLXOCBJSPDZRAMEWNIUYGV";
    public const string RefB = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
    public const string RefC = "FVPJIAOYEDRZXWGCTKUQSBNMHL";
    public const string M4B = "ENKQAUYWJICOPBLMDXZVFTHRGS";
    public const string M4C = "RDOBJNTKVEHMLFCWZAXGYIPSUQ";
}

public enum RotorName
{
    I,
    II,
    III,
    IV,
    V,
    VI,
    VII,
    VIII
}

public enum ReflectorName
{
    RefB,
    RefC,
    M4B,
    M4C
}

// ReSharper restore InconsistentNaming