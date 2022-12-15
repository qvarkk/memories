using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gamemodes { Cube = 0, QTE = 1, Ship = 2 };
public class PlayerController : MonoBehaviour
{
  [SerializeField] private GameObject groundCheck;
  [SerializeField] private LayerMask groundLayer;
  [SerializeField] private GameObject levelObject;
  [SerializeField] private ParticleSystem particle;
  [SerializeField] private GameObject shipSprite;

  public Gamemodes currentGamemode;
  public GameController gameController;
  public Transform sprite;
  public float jumpForce;
  public float shipForce;
  public float jumpBoostForce;
  private Rigidbody2D rb;
  private bool isShip;
  private float lastKnownHeight = 0;

  void Start()
  {
    lastKnownHeight = gameObject.transform.position.y;
    rb = gameObject.GetComponent<Rigidbody2D>();
  }

  void FixedUpdate()
  {
    if (IsGrounded() && !isShip)
    {
      Vector3 spriteRotation = sprite.rotation.eulerAngles;
      spriteRotation.z = Mathf.Round(spriteRotation.z / 90) * 90;
      sprite.rotation = Quaternion.Euler(spriteRotation);
      particle.gameObject.SetActive(true);
      particle.Play();

      if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
      {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
      }
    }
    else if (isShip)
    {
      particle.Pause();
      particle.gameObject.SetActive(false);
      TiltTheShip();
      if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
      {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * shipForce, ForceMode2D.Impulse);
      }
      if (IsGrounded())
      {
        Vector3 spriteRotation = sprite.rotation.eulerAngles;
        spriteRotation.y = Mathf.Round(spriteRotation.y / 90) * 90;
        sprite.rotation = Quaternion.Euler(spriteRotation);
      }
    }
    else
    {
      sprite.Rotate(new Vector3(0, 0, -5));
      particle.Pause();
      particle.gameObject.SetActive(false);
    }
  }


  private void TiltTheShip()
  {
    if (lastKnownHeight > gameObject.transform.position.y && sprite.rotation.y > -0.34)
    {
      sprite.Rotate(new Vector3(0, 0, -3.69f));
    }
    else if (lastKnownHeight < gameObject.transform.position.y && sprite.rotation.y < 0.34)
    {
      sprite.Rotate(new Vector3(0, 0, +1.75f));
    }

    lastKnownHeight = gameObject.transform.position.y;
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
    switch (state)
    {
      case 0:
        rb.gravityScale = 10;
        currentGamemode = gamemode;
        shipSprite.SetActive(false);
        isShip = false;
        break;
      case 1:
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        break;
      case 2:
        sprite.rotation = Quaternion.identity;
        shipSprite.SetActive(true);
        isShip = true;
        break;
    }
  }
}
