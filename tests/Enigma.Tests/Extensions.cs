using System.Text;

namespace Enigma.Tests;

public static class Extensions
{   
    public static string GenerateText(int length)
    {
        var buffer = new StringBuilder(length);
            
        while (length-- > 0)
        {
            var c = Random.Shared.Next(26).ToChar();
            buffer.Append(c);
        }

        return buffer.ToString();
    }
    public static TheoryData<T> ToTheoryData<T>(this IEnumerable<T> data)
    {
        var theoryData = new TheoryData<T>();
        data.ToList().ForEach(x => theoryData.Add(x));
        return theoryData;
    }
    
    public static TheoryData<T1,T2> ToTheoryData<T1,T2>(this IEnumerable<Tuple<T1,T2>> data)
    {
        var theoryData = new TheoryData<T1,T2>();
        data.ToList().ForEach(x => theoryData.Add(x.Item1, x.Item2));
        return theoryData;
    }

    public static TheoryData<T1,T2, T3> ToTheoryData<T1,T2, T3>(this IEnumerable<Tuple<T1,T2, T3>> data)
    {
        var theoryData = new TheoryData<T1,T2, T3>();
        data.ToList().ForEach(x => theoryData.Add(x.Item1, x.Item2, x.Item3));
        return theoryData;
    }

}