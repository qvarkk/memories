using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public float speed = 1f;
    void OnTriggerEnter2D(Collider2D player)
    {
        player.gameObject.GetComponent<Player>().speed = speed;
    }
}
