using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Stats
{
    public int baseValue;

    List<int> modifiers = new List<int>();
    public int GetValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(modifier => finalValue += modifier);
        return finalValue;
    }

    public void AddModifier(int modifier)
    {
        modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        modifiers.Remove(modifier);
    }
}
