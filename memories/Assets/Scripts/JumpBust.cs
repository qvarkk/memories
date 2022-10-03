using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBust : MonoBehaviour
{
    public float jumpBustForce;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().rb.velocity = Vector2.zero;
            other.gameObject.GetComponent<PlayerController>().rb.AddForce(Vector2.up * jumpBustForce, ForceMode2D.Impulse);
        }
    }
}
