using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  [SerializeField] private ParticleSystem deathParticles;
  [SerializeField] private GameObject levelGrid;
  [SerializeField] private GameObject sprite;
  [SerializeField] private GameObject playerParticles;

  public void DeathSequence()
  {
    deathParticles.Play();
    StartCoroutine(DeathSquenceCoroutine());
    levelGrid.GetComponent<LevelController>().enabled = false;
    Destroy(sprite);
    Destroy(playerParticles);
  }

  public IEnumerator DeathSquenceCoroutine()
  {
    yield return new WaitForSeconds(0.5f);

    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
