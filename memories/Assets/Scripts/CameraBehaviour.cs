using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
  [SerializeField] private Transform playerPos;
  private Vector3 camPos;

  private void Start()
  {
    camPos = gameObject.transform.position;
  }

  private void Update()
  {
    if (playerPos.position.y - gameObject.transform.position.y > 3)
    {
      gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.5f, gameObject.transform.position.z), 0.1f);
    }
    else if (playerPos.position.y - gameObject.transform.position.y < -3)
    {
      gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1.5f, gameObject.transform.position.z), 0.1f);
    }
  }
}
