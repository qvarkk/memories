using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBuster : MonoBehaviour
{
    private Rigidbody2D phisic;
    public float jumpforce = 800;
    void OnTriggerEnter2D(Collider2D player)
    {
        phisic = player.gameObject.GetComponent<Rigidbody2D>();
        phisic.AddForce(new Vector3(0, jumpforce, 0));
    }
}
