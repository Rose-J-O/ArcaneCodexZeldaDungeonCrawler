using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWait(float timer)
    {
        if (WaitDictionary.TryGetValue(timer, out var wait)) return wait;

        WaitDictionary[timer] = new WaitForSeconds(timer);
        return WaitDictionary[timer];
    }
}