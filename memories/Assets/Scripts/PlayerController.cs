using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gamemodes { Cube = 0, QTE = 1 };
public class PlayerController : MonoBehaviour
{
  [SerializeField] private GameObject groundCheck;
  [SerializeField] private LayerMask groundLayer;
  [SerializeField] private GameObject levelObject;

  public Gamemodes currentGamemode;
  public GameController gameController;
  public Transform sprite;
  public float jumpForce;
  public float jumpBoostForce;
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

  private void OnTriggerEnter2D(Collider2D collision)
  {
    switch (collision.gameObject.tag)
    {
      case "spike":
        gameController.DeathSequence();
        break;
      case "booster":
        if (collision.gameObject.GetComponent<SpriteRenderer>().flipY == false)
        {
          rb.velocity = Vector2.zero;
          rb.AddForce(Vector2.up * jumpBoostForce * 1.75f, ForceMode2D.Impulse);
        }
        if (collision.gameObject.GetComponent<SpriteRenderer>().flipY == true)
        {
          rb.velocity = Vector2.zero;
          rb.AddForce(Vector2.up * jumpBoostForce * -1.75f, ForceMode2D.Impulse);
        }
        // надо бы задать переменной силу, но мне похуй как то
        break;
      case "speedUp":
        levelObject.GetComponent<LevelController>().speed += 5;
        break;
      case "speedDown":
        levelObject.GetComponent<LevelController>().speed -= 5;
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
