using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCleaner : MonoBehaviour
{
  private void OnCollisionEnter2D(Collision2D collision)
  {
    Destroy(collision.gameObject);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag != "important")
      Destroy(other.gameObject);
  }
}
