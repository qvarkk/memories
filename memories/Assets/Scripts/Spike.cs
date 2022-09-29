using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D player)
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
