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
  [SerializeField] private Sprite W;
  [SerializeField] private Sprite A;
  [SerializeField] private Sprite S;
  [SerializeField] private Sprite D;
  private bool state = false; // на входе в коллизию превращается в тру и на выходе в фолс
  private bool activated = false; // если игрок в коллизии и нажимает нужную кнопку превращается в тру

  void Start()
  {
    if(qteKey == KeyCode.W)
    {
      gameObject.GetComponent<SpriteRenderer>().sprite = W;
      Debug.Log(gameObject.GetComponent<SpriteRenderer>().sprite);
    }
    if(qteKey == KeyCode.A)
    {
      gameObject.GetComponent<SpriteRenderer>().sprite = A;
      Debug.Log(gameObject.GetComponent<SpriteRenderer>().sprite);
    }
    if(qteKey == KeyCode.S)
    {
      gameObject.GetComponent<SpriteRenderer>().sprite = S;
      Debug.Log(gameObject.GetComponent<SpriteRenderer>().sprite);
    }
    if(qteKey == KeyCode.D)
    {
      gameObject.GetComponent<SpriteRenderer>().sprite = D;
      Debug.Log(gameObject.GetComponent<SpriteRenderer>().sprite);
    }
    
  }

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
