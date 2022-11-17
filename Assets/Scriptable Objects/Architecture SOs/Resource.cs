using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Scriptable Objects/Resource")]
public class Resource : ScriptableObject
{
    public string Name;
    public float Value;

    public void Reset()
    {
        Value = 0;
    }

    public void Add(float var)
    {
        Value += var;
    }

    public void Subtract(float var)
    {
        Value -= var;
    }
}
 