using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
  [SerializeField] AddNFTPrompt script;

  public void LoadMainMenu()
  {
    SceneManager.LoadScene(1);
  }

  public void LoadScene(int levelNumber)
  {
    SceneManager.LoadScene(levelNumber);
  }

  public void LoadConstructor()
  {
    SceneManager.LoadScene(3);
  }

  public void ExitGame()
  {
    script.ThrowAConfirmScreen("Вы действительно хотите выйти?", () => {
      Application.Quit();
    });
  }
}
