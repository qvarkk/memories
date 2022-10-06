using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gamemodes { Cube = 0, QTE = 1 };
public class PlayerController : MonoBehaviour
{
  [SerializeField] private GameObject groundCheck;
  [SerializeField] private LayerMask groundLayer;

  public Gamemodes currentGamemode;
  public GameController gameController;
  public Transform sprite;
  public float jumpForce;
  public Rigidbody2D rb;

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
      sprite.Rotate(new Vector3(0, 0, -1));
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
    switch (collision.gameObject.tag)
    {
      case "spike":
        gameController.DeathSequence();
        break;
    }
  }

  public void ChangeThroughPortal(Gamemodes gamemode, int state)
  {
    // короче вродеб пиздатое решение, еще кучу модов можно захуярить и все должно быть збс
    switch (state)
    {
      case 0:
        rb.gravityScale = 10;
        currentGamemode = gamemode;
        break;
      case 1:
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        break;
    }
  }
}
