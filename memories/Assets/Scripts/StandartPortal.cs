using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartPortal : MonoBehaviour
{
    private Rigidbody2D phisic;
    void OnTriggerEnter2D(Collider2D player)
    {
        phisic = player.gameObject.GetComponent<Rigidbody2D>();
        phisic.AddForce(new Vector3(0, 0, 0));
        phisic.constraints = RigidbodyConstraints2D.None;
        phisic.gravityScale = 2;
    }
}
