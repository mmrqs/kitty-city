using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractHealthBar : MonoBehaviour
{
    /// <summary>
    /// bar indicating the life of the player
    /// </summary>
    protected Transform bar;

    /// <summary>
    /// Health of the target
    /// </summary>
    protected float Health { get; set; }

    /// <summary>
    /// Set the size of the health bar
    /// </summary>
    /// <param name="sizeNormalized"></param>
    protected void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    /// <summary>
    /// Makes the health bar flash
    /// </summary>
    protected void ToggleState()
    {
        if (bar.Find(Constants.HEALTH_BAR_SPRITE_LABEL).GetComponent<Image>().color == Color.white)
            bar.Find(Constants.HEALTH_BAR_SPRITE_LABEL).GetComponent<Image>().color = Color.red;
        else
            bar.Find(Constants.HEALTH_BAR_SPRITE_LABEL).GetComponent<Image>().color = Color.white;
    }
}
