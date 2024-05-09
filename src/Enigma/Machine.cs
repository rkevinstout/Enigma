namespace Enigma;

public class Machine
{
    public PlugBoard PlugBoard { get; } = new();
    public Spindle Spindle { get; }
    public Reflector Reflector { get; }
    public readonly List<TraceRecord> Log = [];

    public Machine(Spindle spindle, Reflector reflector)
    {
        Spindle = spindle;
        Reflector = reflector;
    }

    private LinkedList<Pipeline.Step>  BuildPipeline()
    {
        var pipeline = new Pipeline();

        pipeline.Add(PlugBoard);
        pipeline.Add(Spindle.Rotors);
        pipeline.Add(Reflector);

        return pipeline.Build();
    }

    public char Encode(char input)
    {
        Spindle.Advance();
        
        var temp = input;
        
        var pipeline = BuildPipeline();

        foreach (var step in pipeline)
        {
            var result = step.Action.Invoke(temp);
            
            Record(step, temp, result);
            
            temp = result;
        }

        return temp;
    }

    private void Record(Pipeline.Step step, char input, char output)
    {
        var cipher = step.Inbound
            ? step.Component.Cipher
            : step.Component.Cipher.Inversion;

        var alphabet = cipher.ToString()!
            .Replace(output.ToString(), $"[{output}]");
        
        var record = new TraceRecord(
            step.Component.Name,
            input,
            output,
            alphabet
            );
        
        Log.Add(record);
        
    }
}