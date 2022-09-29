using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D phisic;
    private GameObject graundCheck;
    private bool isGraunded;

    public float graundRadius = 0.3f;
    public LayerMask graundMask;
    public float speed = 0.1f;
    public float jumpforce = 400f;

    void Start()
    {
        player = GameObject.Find("player");
        graundCheck = GameObject.Find("GraundCheck");
        phisic = player.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        isGraunded = Physics2D.OverlapCircle(graundCheck.transform.position, graundRadius, graundMask);
        if(Input.GetKeyDown(KeyCode.Space) && isGraunded)
        {
            phisic.AddForce(new Vector3(0, jumpforce, 0));
        }
        player.transform.position += new Vector3(speed, 0, 0);
    }
}
