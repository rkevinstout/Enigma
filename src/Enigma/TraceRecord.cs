namespace Enigma;

public class TraceLog
{
    public readonly List<TraceRecord> Records = [];
    
    public void Record(Pipeline.Step step, char input, char output, string position)
    {
        var cipher = step.Inbound
            ? step.Component.Cipher
            : step.Component.Cipher.Inversion;

        var alphabet = cipher.ToString();
        
        var record = new TraceRecord(
            step.Component.Name,
            input,
            output,
            alphabet!,
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
            var alphabet = Alphabet.Replace(Output.ToString(), $"[{Output}]");
            
            return $"{ComponentName,-7} {Input} -> {alphabet} {Position}";
        }
    }
}