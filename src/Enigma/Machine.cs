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

    private readonly LinkedList<Pipeline.Step> _pipeline;

    public string Position
    {
        get => _spindle.Position;
        set => _spindle.Position = value;
    }

    public readonly TraceLog Log = new();

    private Machine(PlugBoard plugBoard, Spindle spindle, Reflector reflector)
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
        pipeline.Add(_spindle.Rotors.Reverse());
        pipeline.Add(_reflector);

        return pipeline.Build();
    }
    
    public char Enter(char input)
    {
        _spindle.Advance();

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

            Log.Record(step, temp, result, _spindle.Position);

            temp = result;
        }

        return temp;
    }

    private Dictionary<char, char> ToDictionary() =>
        Alphabet.PlainText
            .ToCharArray()
            .ToDictionary(c => c, Encode);

    public SubstitutionCipher ToCipher() => new(ToDictionary());

    public class Configuration
    {
        public List<Rotor> Rotors { get; } = new();
        public string Position { get; } = "AAA";
        public List<PlugBoard.Pair> Pairs { get; } = new();
        public ReflectorName ReflectorName { get; set; } = ReflectorName.RefB;
        public void AddRotor(Rotor rotor) => Rotors.Add(rotor);
        public void AddRotor(RotorName name) => Rotors.Add(RotorFactory.Create(name));
        public void AddRotors(params RotorName[] names) => names.ToList().ForEach(AddRotor);
        public void AddPairs(params PlugBoard.Pair[] pairs) => pairs.ToList().ForEach(Pairs.Add);

        public Machine Create() =>
            new(
                new PlugBoard(Pairs.ToArray()),
                new Spindle(Rotors.ToArray()),
                Reflector.Create(ReflectorName)
            );
    }
}