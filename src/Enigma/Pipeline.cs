using System.Collections.ObjectModel;

namespace Enigma;

public class Pipeline
{
    private readonly List<string> _log;
    private readonly List<ICipher> _steps = [];

    public ReadOnlyCollection<ICipher> Steps => _steps.AsReadOnly();
    public void Add(ICipher cipher) => _steps.Add(cipher);
    public void AddRange(IEnumerable<ICipher> ciphers) => _steps.AddRange(ciphers);

    public Pipeline(List<string> log) => _log = log;

    public LinkedList<Func<char, char>> Build()
    {
        var stack = new Stack<ICipher>(_steps);
        
        var list = new LinkedList<Func<char, char>>();

        if (stack.Count == 0) return list;

        var step = stack.Pop();

        list.AddFirst(x => step.Encode(x));

        while (stack.Count > 0)
        {
            var next = stack.Pop();

            list.AddBefore(list.First!, x => next.Encode(x));
            list.AddAfter(list.Last!, x => next.Decode(x));
        }
        
        return list;
    }

    public char Execute(char input)
    {
        var temp = input;
        
        for (var i = 0; i < _steps.Count; i++)
        {
            var cipher = _steps[i];

            var result = cipher.Encode(temp);
            
            _log.Add($"{i} {cipher.Name} {temp} -> {result}");

            temp = result;
        }
        
        // skip decoding the reflector as it would undo
        // it's own encoding
        for (var i = _steps.Count - 2; i >= 0; i--)
        {
            var cipher = _steps[i];

            var result = cipher.Decode(temp);
            
            _log.Add($"{i} {cipher.Name} {temp} -> {result}");

            temp = result;
        }

        return temp;
    }
}