namespace Enigma;

public class Machine
{
    public PlugBoard PlugBoard { get; } = new();
    public Spindle Spindle { get; }
    public Reflector Reflector { get; }
    public readonly TraceLog Log = new();

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
        
        var pipeline = BuildPipeline();
        
        var temp = input;

        var node = pipeline.First;
        
        while (node != null)
        {
            var step = node.Value;

            var result = step.Action.Invoke(temp);

            Log.Record(step, temp, result, Spindle.Position);

            temp = result;
            node = node.Next;
        }

        return temp;
    }
}