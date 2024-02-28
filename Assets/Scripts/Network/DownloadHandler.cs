using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Download Handler",menuName ="Network/Download Handler")]
public class DownloadHandler: ScriptableObject
{
    public static void LoadRecipeImage(string imageName, string imageUrl, RawImage rawImage, UnityAction actionToExecute = null)
    {
        string directoryPath = Path.Combine(Application.persistentDataPath, "/Images/");
        string imageLocalPath = Path.Combine(directoryPath  , imageName);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        if (File.Exists(imageLocalPath))
        {
            rawImage.texture = LoadLocalTexture(imageLocalPath);
        }
        else
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);

            www.SendWebRequest().AsAsyncOperationObservable().Subscribe(_ =>
            {
                if (www.result == UnityWebRequest.Result.Success)
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(www);

                    SaveTextureLocally(texture,imageLocalPath);    

                    rawImage.texture = texture;
                }
                else
                {
                    Debug.LogError("Failed to load recipe image: " + www.error);
                }
            });
        }

        actionToExecute?.Invoke();
    }

    private static Texture2D LoadLocalTexture(string imagePath)
    {
        byte[] bytes = File.ReadAllBytes(imagePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);

        return texture;
    }

    private static void SaveTextureLocally(Texture2D tex,string imagePath)
    {
        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(imagePath, bytes);
    }

}
