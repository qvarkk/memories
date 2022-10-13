using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlTrigger : MonoBehaviour
{
   [SerializeField] private float colorChangeSpeed;
   [SerializeField] private float angleChangeSpeed;
   [SerializeField] private Color newColor;
   [SerializeField] private float angle;
   [SerializeField] private float zoomForce;
   [SerializeField] private GameObject Camera;
   private bool isChangingColor = false;
   private bool isChangingAngle = false;

   void OnTriggerEnter2D(Collider2D other)
   {
    if(other.gameObject.tag == "Player")
    {
        Debug.Log("вошёл ёпта");
        isChangingColor = true;
        isChangingAngle = true;
        Camera.GetComponent<Camera>().orthographicSize = Camera.GetComponent<Camera>().orthographicSize / zoomForce;
    }
    
   }
   void Update()
   {
    if(isChangingColor == true)
    {
        Camera.GetComponent<Camera>().backgroundColor = Color.Lerp(Camera.GetComponent<Camera>().backgroundColor, newColor, colorChangeSpeed * Time.deltaTime);
        if(Camera.GetComponent<Camera>().backgroundColor == newColor)
        {
            isChangingColor = false;
        }
    }
    if(isChangingAngle == true)
    {
        Camera.transform.rotation = Quaternion.Lerp(Camera.transform.rotation, Quaternion.Euler(0, 0, angle), angleChangeSpeed * Time.deltaTime);
        if(Camera.transform.rotation == Quaternion.Euler(0, 0, angle))
        {
            isChangingAngle = false;
        }
    }
   }
}
