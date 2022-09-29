using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScale : MonoBehaviour
{
    public void ButtonScales(Transform button)
    {
        button.localScale = new Vector3(1.2f, 1.2f, 1f);
    } 
    public void ButtonScaleReturn(Transform button)
    {
        button.localScale = new Vector3(1f, 1f, 1f);
    } 
}
