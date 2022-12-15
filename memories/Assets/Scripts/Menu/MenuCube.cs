using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuCube : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject sprite;
    private Color cubeColor;
    private SpriteRenderer spriteRenderer;
    private GameObject instantiator;
    private Rigidbody2D rb;
    private float timer; 

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        cubeColor = new Color(Random.value, Random.value, Random.value);
        spriteRenderer.color = cubeColor;

        timer = Random.value;
        InvokeRepeating("Jump", 0f, timer);
        instantiator = GameObject.Find("Instantiator");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(gameObject);
        instantiator.GetComponent<MenuInstantiator>().count += 1;
    }

    private void Update()
    {
        if (IsGrounded()) 
        {
            Vector3 spriteRotation = sprite.transform.rotation.eulerAngles;
            spriteRotation.z = Mathf.Round(spriteRotation.z / 90) * 90;
            sprite.transform.rotation = Quaternion.Euler(spriteRotation);
        }
        else
        {
            sprite.transform.Rotate(new Vector3(0, 0, -5));
        }
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
}

