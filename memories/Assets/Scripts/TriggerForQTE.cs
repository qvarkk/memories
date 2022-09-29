using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerForQTE : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("player");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("a");
        if(other.gameObject.GetComponent<QTE>()._activated == false && other.tag == "QTE")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void Update()
    {
        gameObject.transform.position = player.GetComponent<Player>().transform.position;
        gameObject.transform.position += new Vector3(-8f, 0, 0);
    }
}
