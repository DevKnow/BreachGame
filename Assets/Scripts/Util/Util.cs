using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    /// <summary>
    /// Returns a list of random elements from the source list (no duplicates).
    /// </summary>
    public static List<T> GetRandomElements<T>(this List<T> source, int count)
    {
        var shuffled = new List<T>(source);

        for (int i = shuffled.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
        }

        return shuffled.GetRange(0, Mathf.Min(count, shuffled.Count));
    }
}
