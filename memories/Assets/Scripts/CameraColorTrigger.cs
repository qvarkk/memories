using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColorTrigger : MonoBehaviour
{
  [SerializeField] private Color newColor;
  [SerializeField][Range(1, 6)] float colorSpeed = 2f;
  [SerializeField] private Camera cam;
  [SerializeField][Range(0, 2)] private float zoomForce = 1f;
  CameraBehaviour camScript;

  void Start()
  {
    camScript = cam.GetComponent<CameraBehaviour>();
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    StartCoroutine(camScript.ChangeCameraColor(newColor, colorSpeed));
    if (other.gameObject.tag == "Player")
      cam.GetComponent<Camera>().orthographicSize = cam.GetComponent<Camera>().orthographicSize / zoomForce;
  }
}
