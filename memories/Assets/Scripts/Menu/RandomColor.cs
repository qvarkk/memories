using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    public Color color;
    void Start()
    {
        var r = Random.Range(0, 255);
        var g = Random.Range(0, 255);
        var b = Random.Range(0, 255);
        color = new Color(r, g, b, 255);
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }
}
