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
    private GameObject instantiator;
    private Rigidbody2D rb;
    private float timer; 

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
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

