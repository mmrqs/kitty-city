using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FilteredFlockBehavior : FlockBehavior
{
    /// <summary>
    /// Filter of the near by objects.
    /// </summary>
    public ContextFilter filter;
}
