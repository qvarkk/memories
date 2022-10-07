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
<<<<<<< Updated upstream
  [SerializeField] private KeyCode qteKey;
  private bool state = false;
  private bool activated = false;
=======
  // если код устраивает, можно нахуй снести все выше

  [SerializeField] private KeyCode qteKey;
  private bool state = false; // на входе в коллизию превращается в тру и на выходе в фолс
  private bool activated = false; // если игрок в коллизии и нажимает нужную кнопку превращается в тру
>>>>>>> Stashed changes

  private void OnTriggerEnter2D(Collider2D other)
  {
    state = true;
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (!activated)
    {
<<<<<<< Updated upstream
      Debug.Log("Didn't activate");
      other.gameObject.GetComponent<PlayerController>().gameController.DeathSequence();
    }
    state = false;
=======
      Debug.Log("Haven't activated in time");
      other.gameObject.GetComponent<PlayerController>().gameController.DeathSequence();
    }
    state = false;
    gameObject.GetComponent<Collider2D>().isTrigger = false; // чтоб обджект клинер не триггерил его еще раз
>>>>>>> Stashed changes
  }

  private void Update()
  {
<<<<<<< Updated upstream
    if (state && Input.GetKeyDown(qteKey))
    {
      Debug.Log("Activated");
=======
    if (Input.GetKeyDown(qteKey) && state)
    {
      Debug.Log("Pressed succesfully");
>>>>>>> Stashed changes
      activated = true;
    }
  }
}
