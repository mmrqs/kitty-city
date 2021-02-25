using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the health bar of the player
/// </summary>
public class HealthBar : AbstractHealthBar
{
    /// <summary>
    /// Player
    /// </summary>
    public Player target;

    /// <summary>
    /// Start method
    /// Initializes the health bar
    /// </summary>
    void Start()
    {
        bar = transform.Find(Constants.HEALTH_BAR_LABEL);
    }

    /// <summary>
    /// Update method
    /// Update the health bar.
    /// If the health of the player is low, makes it makes the healthbar flash
    /// </summary>
    void Update()
    {
        Health = (float)target.Life;
        SetSize(Health / 100);
    
        if (Health/100 < 0.5f)
            InvokeRepeating("ToggleState", 0.5f, 1f);
    }
}
