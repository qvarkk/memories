using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public void ButtonScale(Transform button)
    {
        button.localScale = new Vector3(1.2f, 1.2f, 1f);
    } 
        public void ButtonScaleReturn(Transform button)
    {
        button.localScale = new Vector3(1f, 1f, 1f);
    } 
}
