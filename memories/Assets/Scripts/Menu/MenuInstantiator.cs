using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject cube;

    private void Start()
    {
        InvokeRepeating("InstantiateCube", 0f, 3f);
    }

    private void InstantiateCube()
    {
        Instantiate(cube, transform);
    }
}
