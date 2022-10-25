using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
  [SerializeField] private Gamemodes gamemode;
  [SerializeField] private int state;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    try
    {
      PlayerController player = collision.gameObject.GetComponent<PlayerController>();

      player.ChangeThroughPortal(gamemode, state);
    }
    catch { }
  }
}
