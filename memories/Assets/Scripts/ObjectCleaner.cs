using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCleaner : MonoBehaviour
{
  public GameController gameController;
  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "QTE")
    {
      if (collision.gameObject.GetComponent<QTE>()._activated == false && collision.gameObject.tag == "QTE")
      {
        Debug.Log("тригер не активирован");
        gameController.DeathSequence();
      }
    }
    else
    {
      Destroy(collision.gameObject);
    }
  }
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "QTE")
    {
      if (other.gameObject.GetComponent<QTE>()._activated == false)
      {
        Debug.Log("тригер не активирован");
        gameController.DeathSequence();
      }
    }
    if (other.gameObject.tag != "ground")
    {
      Destroy(other.gameObject);
    }
  }
}
