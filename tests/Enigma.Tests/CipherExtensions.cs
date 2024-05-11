using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Enigma.Tests;

public static class CipherExtensions
{
    public static CipherAssertions Should(this ICipher instance)
    {
        return new CipherAssertions(instance);
    }
}

public class CipherAssertions(ICipher subject) 
    : ReferenceTypeAssertions<ICipher, CipherAssertions>(subject)
{
    protected override string Identifier => "Cipher";

    public AndConstraint<CipherAssertions> BeSelfReciprocal()
    {
        // Execute.Assertion
        //     .Given(() => Subject.ToDictionary()).
        //     .ForCondition(d => d.Keys.);

        return new AndConstraint<CipherAssertions>(this);
    }
}