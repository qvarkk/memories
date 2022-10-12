using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
  [SerializeField] private Gamemodes gamemode;
  [SerializeField] private int state;

  private void OnCollisionEnter2D(Collision2D collision)
  {
    // честно эту хуйню запихнул чисто выебнуться, что выучил, но похуй, заебись смотрится, типа я что-то знаю
    try
    {
      PlayerController player = collision.gameObject.GetComponent<PlayerController>();

      player.ChangeThroughPortal(gamemode, state);
    }
    catch { }
  }
}
