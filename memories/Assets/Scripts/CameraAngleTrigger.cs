using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngleTrigger : MonoBehaviour
{
  [SerializeField][Range(0, 360)] private int newAngle;
  [SerializeField][Range(1, 6)] float angleSpeed = 2f;
  [SerializeField] private Camera cam;
  CameraBehaviour camScript;

  void Start()
  {
    camScript = cam.GetComponent<CameraBehaviour>();
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    StartCoroutine(camScript.ChangeCameraAngle(newAngle, angleSpeed));
  }
}
