namespace Enigma;

public record TraceRecord(string ComponentName, char Input, char Output, string Alphabet)
{
    public override string ToString() => 
        $"{ComponentName, -7} {Input} -> {Alphabet}";
}