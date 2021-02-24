using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    public Player target;
    private float Health { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("Bar");
    }

    void Update()
    {
        Health = (float)target.Life;
        SetSize(Health / 100);
    
        if (Health/100 < 0.5f)
            InvokeRepeating("ToggleState", 0.5f, 1f);
    }

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    public void SetColor(Color color)
    {
        bar.Find("BarSprite").GetComponent<Image>().color = color;
    }

    public void ToggleState()
    {
        if (bar.Find("BarSprite").GetComponent<Image>().color == Color.white)
            bar.Find("BarSprite").GetComponent<Image>().color = Color.red;
        else
            bar.Find("BarSprite").GetComponent<Image>().color = Color.white;
    }
}
