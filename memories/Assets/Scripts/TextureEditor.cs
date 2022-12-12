using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class TextureEditor : MonoBehaviour
{
    [SerializeField] private Texture2D texture;
    [SerializeField] private FilterMode filterMode;
    [SerializeField] private TextureWrapMode textureWrapMode;
    [SerializeField] [Range(2,512)] private int textureSize = 512;
    [SerializeField] private Material material;
    [SerializeField] private Camera _camera;
    [SerializeField] private Collider _collider;
    [SerializeField] private Image brushColor;
    [SerializeField] private Slider sizeValue;
    [SerializeField] private Slider redValue;
    [SerializeField] private Slider greenValue;
    [SerializeField] private Slider blueValue;
    [SerializeField] private Slider alphaValue;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private AddNFTPrompt script;

    private int clickCount = 0;
    private int oldReyX, oldReyY;

    void OnValidate()
    {
        if(texture == null)
        {
            texture = new Texture2D(textureSize, textureSize);
        }
        
        if(texture.width != textureSize)
        {
            texture.Reinitialize(textureSize, textureSize);
        }
        texture.filterMode = filterMode;
        texture.wrapMode = textureWrapMode;
        material.mainTexture = texture;
        texture.Apply();
    }

    void Update()
    {
        int brushSize = (int)(sizeValue.value * textureSize);
        Color activeColor = new Color(redValue.value, greenValue.value, blueValue.value, alphaValue.value);
        if(brushColor.color != activeColor)
        {
            brushColor.color = activeColor;
        }
        if(Input.GetMouseButton(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(_collider.Raycast(ray, out hit, 100f))
            {
                int rayX = (int)(hit.textureCoord.x * textureSize);
                int rayY = (int)(hit.textureCoord.y * textureSize);
                if(oldReyX != rayX || oldReyY != rayY)
                {
                    for (int y = 0; y < sizeValue.value * textureSize; y++)
                    {
                        for (int x = 0; x < sizeValue.value * textureSize; x++)
                        {
                            float x2 = Mathf.Pow(x - brushSize / 2, 2);
                            float y2 = Mathf.Pow(y - brushSize / 2, 2);
                            float r2 = Mathf.Pow(brushSize / 2 - 0.5f, 2);
                            if(x2 + y2 < r2)
                            {
                                int pixelX = rayX + x - brushSize / 2;
                                int pixelY = rayY + y - brushSize / 2;

                                if(pixelX >= 0 && pixelX < textureSize && pixelY >= 0 && pixelY < textureSize)
                                {
                                    Color oldColor = texture.GetPixel(pixelX, pixelY);
                                    Color resultColor = Color.Lerp(oldColor, activeColor, activeColor.a);
                                    texture.SetPixel(pixelX, pixelY, resultColor);
                                    clickCount++;
                                }
                            }
                        } 
                    }
                    oldReyX = rayX;
                    oldReyY = rayY;
                }
                texture.Apply();
            }
        }
    }

    public void SavePlayerImage()
    {
        script.ThrowAConfirmScreen("Вы действительно хотите сохранить картинку?", () => {
            if (nameInput.text != "" && clickCount > 0)
            {
                if (PlayerPrefs.GetInt("TexturesQuantity") > 5)
                {
                    script.ThrowAnError("Лимит картинок 5 штук, попробуйте очистить список");
                    return;
                }

                byte[] textureBytes = texture.EncodeToPNG();
                File.WriteAllBytes(Application.persistentDataPath + "/playerTexture" + (PlayerPrefs.GetInt("TexturesQuantity") + 1).ToString() + ".png", textureBytes);

                PlayerPrefs.SetInt("TexturesQuantity", PlayerPrefs.GetInt("TexturesQuantity") + 1);
                PlayerPrefs.SetString("TextureName" + (PlayerPrefs.GetInt("TexturesQuantity")).ToString(), nameInput.text);

                SceneManager.LoadScene(1);
            }
            else
            {
                script.ThrowAnError("Задайте имя для вашего рисунка");
            }
        });
    }

    public void ReturnToMenu()
    {
        script.ThrowAConfirmScreen("Вы действительно хотите вернуться?", () => {SceneManager.LoadScene(1); Debug.Log("qqqq");});
    }
}
