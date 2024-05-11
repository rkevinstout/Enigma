namespace Enigma;

/// <summary>
/// Represents the functional elements of
/// the Enigma machine
/// </summary>
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
        pipeline.Add(Spindle.Rotors.Reverse());
        pipeline.Add(Reflector);

        return pipeline.Build();
    }
    
    public char Enter(char input)
    {
        Spindle.Advance();

        return Encode(input);
    }
    
    public char Encode(char input)
    {
        var pipeline = BuildPipeline();

        return Encode(pipeline, input);
    }

    private char Encode(LinkedList<Pipeline.Step> pipeline, char input)
    {
        var temp = input;

        foreach (var step in pipeline)
        {
            var result = step.Action.Invoke(temp);

            Log.Record(step, temp, result, Spindle.Position);

            temp = result;
        }

        return temp;
    }
    
    public Dictionary<char, char> ToDictionary() =>
        Alphabet.PlainText
            .ToCharArray()
            .ToDictionary(c => c, Encode);

    public SubstitutionCipher ToCipher() => new(ToDictionary());
}