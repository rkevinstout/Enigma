namespace Enigma;

public class TraceLog
{
    public List<TraceRecord> Records = [];

    public void Reset() => Records = [];
    
    public void Record(Pipeline.Step step, char input, char output, string position)
    {
        var map = step.Inbound 
            ? step.Component.CharacterMap 
            : step.Component.InvertedMap;
        
        var record = new TraceRecord(
            step.Component.Name,
            input,
            output,
            map.ToString(),
            position
        );
        
        Records.Add(record);
    }
    
    public record TraceRecord(
        string ComponentName,
        char Input,
        char Output,
        string Alphabet,
        string Position
    )
    {
        public override string ToString()
        {
            var alphabet = Alphabet.Replace(Output.ToString(), $"_{Output}_");
            
            return $"{ComponentName,-7} {Input} -> {Output} {alphabet} {Position}";
        }
    }
}