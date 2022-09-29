using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void OpenLevelOne()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenCnstructor()
    {
        SceneManager.LoadScene(2);
    }

    public void CloseLevel()
    {
        SceneManager.LoadScene(0);
    }
}
