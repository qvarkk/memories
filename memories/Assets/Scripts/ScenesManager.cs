using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
  public void LoadMainMenu()
  {
    SceneManager.LoadScene(1);
  }

  public void LoadLevel(int levelNumber)
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + levelNumber);
  }

  public void LoadConstructor()
  {
    SceneManager.LoadScene(3);
  }

  public void ExitGame()
  {
    // на билде главное не забыть убрать некст линию
    UnityEditor.EditorApplication.isPlaying = false;
    Application.Quit();
  }
}
