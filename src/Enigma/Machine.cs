namespace Enigma;

public class Machine
{
    public PlugBoard PlugBoard { get; } = new();
    public Spindle Spindle { get; }
    public Reflector Reflector { get; }
    public readonly List<string> Log = [];

    public Machine(Spindle spindle, Reflector reflector)
    {
        Spindle = spindle;
        Reflector = reflector;
    }

    private Pipeline BuildPipeline()
    {
        var pipeline = new Pipeline(Log);

        pipeline.Add(PlugBoard);
        pipeline.AddRange(Spindle.Rotors);
        pipeline.Add(Reflector);

        return pipeline;
    }

    public char Encode(char input)
    {
        Spindle.Advance();
        
        var temp = input;
        
        var pipeline = BuildPipeline();

        var list = pipeline.Build();

        foreach (var step in list)
        {
            var result = step.Invoke(temp);
            Log.Add($"{temp} -> {result}");
            temp = result;
        }

        //var result =  pipeline.Execute(input);

        return temp;
    }
}