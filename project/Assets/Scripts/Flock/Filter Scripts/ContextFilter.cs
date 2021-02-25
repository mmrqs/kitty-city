using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class that aims to filter objects
/// </summary>
public abstract class ContextFilter : ScriptableObject
{
    /// <summary>
    /// Allow filter
    /// </summary>
    /// <param name="agent">flock agent</param>
    /// <param name="original">list of near by object of the flock agent</param>
    /// <returns></returns>
    public abstract List<Transform> Filter(FlockAgent agent, List<Transform> original);
}
