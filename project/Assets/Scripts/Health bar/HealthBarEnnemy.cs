using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manages the health bar of the ennemy
/// </summary>
public class HealthBarEnnemy : AbstractHealthBar
{
    /// <summary>
    /// Ennemy
    /// </summary>
    public Ennemy target;

    /// <summary>
    /// Start method
    /// Initializes the health bar
    /// </summary>
    void Start()
    {
        bar = transform.Find(Constants.HEALTH_BAR_ENNEMY);
    }

    /// <summary>
    /// Update method
    /// Update the health bar.
    /// If the health of the ennemy is low, makes it makes the healthbar flash
    /// </summary>
    void Update()
    {
        Health = (float)target.Life;
        SetSize(Health / 100);

        if (Health / 100 < 0.5f)
            InvokeRepeating("ToggleState", 0.5f, 1f);
    }
}
