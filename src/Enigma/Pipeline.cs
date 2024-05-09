namespace Enigma;

public class Pipeline
{
    private readonly List<IComponent> _components = [];

    public void Add(IComponent component) => _components.Add(component);
    public void AddRange(IEnumerable<IComponent> components) => _components.AddRange(components);

    public LinkedList<Step> Build()
    {
        var stack = new Stack<IComponent>(_components);
        
        var list = new LinkedList<Step>();

        if (stack.Count == 0) return list;

        var component = stack.Pop();

        list.AddFirst(component.CreateStep(x => component.Cipher.Encode(x)));

        while (stack.Count > 0)
        {
            var next = stack.Pop();

            list.AddBefore(list.First!, next.CreateStep(x => next.Cipher.Encode(x)));
            list.AddAfter(list.Last!, next.CreateStep(x => next.Cipher.Decode(x)));
        }
        
        return list;
    }

    public class Step
    {
        public Step(IComponent component, Func<char, char> action)
        {
            Component = component;
            Action = action;
        }

        public IComponent Component { get; }
        public Func<char, char> Action { get; }
    }
}