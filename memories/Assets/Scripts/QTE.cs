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
  // если код устраивает, можно нахуй снести все выше

  [SerializeField] private KeyCode qteKey;
  [SerializeField] private Color activatedColor;
  private bool state = false; // на входе в коллизию превращается в тру и на выходе в фолс
  private bool activated = false; // если игрок в коллизии и нажимает нужную кнопку превращается в тру

  private void OnTriggerEnter2D(Collider2D other)
  {
    state = true;
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (!activated)
    {
      Debug.Log("Haven't activated in time");
      other.gameObject.GetComponent<PlayerController>().gameController.DeathSequence();
    }
    state = false;
    gameObject.GetComponent<Collider2D>().isTrigger = false; // чтоб обджект клинер не триггерил его еще раз
  }

  private void Update()
  {
    if (Input.GetKeyDown(qteKey) && state)
    {
      Debug.Log("Pressed succesfully");
      gameObject.GetComponent<SpriteRenderer>().color = activatedColor;
      activated = true;
    }
  }
}
