using System;
using System.Collections;
using System.Collections.Generic;

public static class Extensions
{
    
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        // https://stackoverflow.com/questions/273313/randomize-a-listt

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

    }

    // https://discussions.unity.com/t/using-random-range-to-pick-a-random-value-out-of-an-enum/119639/3
    public static T RandomEnumValue<T>()
    {
        var values = Enum.GetValues(typeof(T));
        int random = UnityEngine.Random.Range(0, values.Length);
        return (T)values.GetValue(random);
    }
}
