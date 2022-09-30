using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public float speed;

    void Update()
    {
       gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
    }
}
