using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCleaner : MonoBehaviour
{
  public GameController gameController;
  private void OnCollisionEnter2D(Collision2D collision)
  {
    Destroy(collision.gameObject);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag != "QTE")
      Destroy(other.gameObject);
  }
}
