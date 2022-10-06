using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInstantiator : MonoBehaviour
{
  [SerializeField] private GameObject cube;
  [SerializeField] private GameObject prikol;
  private bool flag = false;
  public int count;

  private void Start()
  {
    InvokeRepeating("InstantiateCube", 0f, 2f);
  }

  private void InstantiateCube()
  {
    Instantiate(cube, transform);
  }

  void Update()
  {
    if (count == 5 && flag == false)
    {
      Instantiate(prikol, new Vector3(0, 5, 1), Quaternion.identity);
      flag = true;
    }
  }
}
