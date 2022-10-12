using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    [SerializeField]private float speedup;
    private GameObject levelController;

    void Start()
    {
        levelController = GameObject.Find("Level Objects");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        levelController.GetComponent<LevelController>().speed = levelController.GetComponent<LevelController>().speed * speedup;
    }
}
