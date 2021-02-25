using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Filter that aims to avoid obstacles
/// </summary>
[CreateAssetMenu(menuName = "Flock/Filter/Physics Layer")]
public class PhysicsLayerFilter : ContextFilter
{
    /// <summary>
    /// Mask of obstacles
    /// </summary>
    public LayerMask mask;

    /// <summary>
    /// Filters obstacles
    /// </summary>
    /// <param name="agent">flock agent</param>
    /// <param name="original">near by object of the flock agent</param>
    /// <returns></returns>
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        // foreach near by objects
        foreach (Transform item in original)
        {
            // we check if the mask equals the layer of the item
            if (mask == (mask | (1 << item.gameObject.layer)))
                filtered.Add(item);
        }
        return filtered;
    }
}