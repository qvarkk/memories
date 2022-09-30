using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private GameObject groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public GameController gameController;
    public Transform sprite;
    public float jumpForce;
    public float jumpBustForce;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsGrounded())
        {
            Vector3 spriteRotation = sprite.rotation.eulerAngles;
            spriteRotation.z = Mathf.Round(spriteRotation.z / 90) * 90;
            sprite.rotation = Quaternion.Euler(spriteRotation);

            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        else
        {
            sprite.Rotate(Vector3.back * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        if (Physics2D.OverlapCircle(groundCheck.transform.position, 0.05f, groundLayer))
        {
            return true;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "spike")
        {
            gameController.DeathSequence();
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "jumpBuster")
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpBustForce, ForceMode2D.Impulse);
        }
        if (other.gameObject.name == "QTE Portal")
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.gravityScale = 0;
        }
    }
}
