using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
  [SerializeField] Transform playerPos;
  Camera cum;

  private void Start()
  {
    cum = gameObject.GetComponent<Camera>();
  }

  public IEnumerator ChangeCameraColor(Color newColor, float colorSpeed)
  {
    Color startColor = cum.backgroundColor;
    float colorChangePercentage = 0f;

    while (colorChangePercentage < 1f)
    {
      colorChangePercentage += Time.deltaTime * colorSpeed;
      cum.backgroundColor = Color.Lerp(startColor, newColor, colorChangePercentage);
      yield return new WaitForEndOfFrame();
    }
  }

  public IEnumerator ChangeCameraAngle(int newAngle, float angleSpeed)
  {
    Quaternion angleNow = cum.transform.rotation;
    float angleChangePercentage = 0f;

    while (angleChangePercentage < 1f)
    {
      angleChangePercentage += Time.deltaTime * angleSpeed;
      cum.transform.rotation = Quaternion.Lerp(angleNow, Quaternion.Euler(0, 0, newAngle), angleChangePercentage);
      yield return new WaitForEndOfFrame();
    }
  }
}
