using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTE : MonoBehaviour
{
  [SerializeField] private Color activatedColor;
  [SerializeField] private KeyCode keyCode;
  [SerializeField] private Sprite[] sprites;
  private KeyCode[] keyCodes = new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
  private int index;
  private bool state = false; // на входе в коллизию превращается в тру и на выходе в фолс
  private bool activated = false; // если игрок в коллизии и нажимает нужную кнопку превращается в тру

  void Start()
  {
    for (int i = 0; i < keyCodes.Length; i++)
    {
      if (keyCodes[i] == keyCode)
      {
        index = i;
        break;
      }
    }

    gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[index];
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
      try
      {
        other.gameObject.GetComponent<PlayerController>().gameController.DeathSequence();
      }
      catch { }
    }
    state = false;
    gameObject.GetComponent<Collider2D>().isTrigger = false; // чтоб обджект клинер не триггерил его еще раз
  }

  private void Update()
  {
    if (Input.GetKeyDown(keyCode) && state)
    {
      Debug.Log("Pressed succesfully");
      gameObject.GetComponent<SpriteRenderer>().color = activatedColor;
      gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = activatedColor;
      activated = true;
    }
  }
}
