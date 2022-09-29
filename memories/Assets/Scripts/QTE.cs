using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTE : MonoBehaviour
{
    [SerializeField]private Color activated;
    public bool _activated = false;
    private bool flage = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        flage = true;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && flage == true && gameObject.name == "QTE Trigger W")
        {
            gameObject.GetComponent<SpriteRenderer>().color = activated;
            _activated = true;
        }
        if(Input.GetKeyDown(KeyCode.A) && flage == true && gameObject.name == "QTE Trigger A")
        {
            gameObject.GetComponent<SpriteRenderer>().color = activated;
            _activated = true;
        }
        if(Input.GetKeyDown(KeyCode.S) && flage == true && gameObject.name == "QTE Trigger S")
        {
            gameObject.GetComponent<SpriteRenderer>().color = activated;
            _activated = true;
        }
        if(Input.GetKeyDown(KeyCode.D) && flage == true && gameObject.name == "QTE Trigger D")
        {
            gameObject.GetComponent<SpriteRenderer>().color = activated;
            _activated = true;
        }
        
    }
}
