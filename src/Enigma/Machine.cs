namespace Enigma;

/// <summary>
/// Represents the functional elements of
/// the Enigma machine
/// </summary>
public class Machine
{
    private readonly PlugBoard _plugBoard;
    private readonly Spindle _spindle;
    private readonly Reflector _reflector;

    public bool Debug { get; set; } = false;

    private readonly LinkedList<Pipeline.Step> _pipeline;

    public string Position
    {
        get => _spindle.Position;
        set => _spindle.Position = value;
    }

    public readonly TraceLog Log = new();

    public Machine(
        PlugBoard plugBoard, 
        Spindle spindle, 
        Reflector reflector
        )
    {
        _plugBoard = plugBoard;
        _spindle = spindle;
        _reflector = reflector;
        
        _pipeline = BuildPipeline();
    }

    private LinkedList<Pipeline.Step>  BuildPipeline()
    {
        var pipeline = new Pipeline();

        pipeline.Add(_plugBoard);
        pipeline.Add(_spindle.Rotors);
        pipeline.Add(_reflector);

        return pipeline.Build();
    }
    
    public char Enter(char input)
    {
        _spindle.Advance();

        return Encode(input);
    }
    
    public char Encode(char input) => Encode(_pipeline, input);

    private char Encode(LinkedList<Pipeline.Step> pipeline, char input)
    {
        var temp = input;

        foreach (var step in pipeline)
        {
            var result = step.Execute(temp);

            if (Debug)
                Log.Record(step, temp, result, _spindle.Position);

            temp = result;
        }

        return temp;
    }
    
    public override string ToString() => $"{_reflector.Name} {_spindle} {_spindle.Rings} [{_plugBoard}] {_spindle.Position}";
}