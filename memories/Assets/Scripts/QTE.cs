using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTE : MonoBehaviour
{
  // [SerializeField]private Color activated;
  // public bool _activated = false;
  // private bool flage = false;
  // [SerializeField]private KeyCode UserKey = KeyCode.W;

  // void OnTriggerEnter2D(Collider2D other)
  // {
  //     flage = true;
  // }
  // void OnTriggerExit2D(Collider2D other)
  // {
  //     flage = false;
  // }
  // void Update()
  // {
  //     if(Input.GetKeyDown(UserKey) && flage == true)
  //     {
  //         gameObject.GetComponent<SpriteRenderer>().color = activated;
  //         _activated = true;
  //     }

  // }
  [SerializeField] private KeyCode qteKey;
  private bool state = false;
  private bool activated = false;

  private void OnTriggerEnter2D(Collider2D other)
  {
    state = true;
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (!activated)
    {
      Debug.Log("Didn't activate");
      other.gameObject.GetComponent<PlayerController>().gameController.DeathSequence();
    }
    state = false;
  }

  private void Update()
  {
    if (state && Input.GetKeyDown(qteKey))
    {
      Debug.Log("Activated");
      activated = true;
    }
  }
}
