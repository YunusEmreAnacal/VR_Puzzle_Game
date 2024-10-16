using UnityEditor;
using UnityEngine;
using System.IO;

public class MirrorManagerEditor : EditorWindow
{
    [MenuItem("Tools/Mirror Manager")]
    public static void ShowWindow()
    {
        GetWindow<MirrorManagerEditor>("Mirror Manager");
    }

    private void OnGUI()
    {
        GUILayout.Label("Mirror Manager", EditorStyles.boldLabel);

        if (GUILayout.Button("Generate RenderTextures for All Mirrors"))
        {
            GenerateRenderTexturesForAllMirrors();
        }
    }

    private void GenerateRenderTexturesForAllMirrors()
    {
        // Sahnedeki t�m kameralar� buluyoruz
        Camera[] allCameras = FindObjectsOfType<Camera>();

        foreach (Camera cam in allCameras)
        {
            // Kameran�n ba�l� oldu�u objenin "Mirror" ad�n� ta��y�p ta��mad���n� kontrol ediyoruz
            if (cam.gameObject.tag.Contains("Mirror"))
            {
                // Yeni bir RenderTexture olu�turuyoruz
                RenderTexture renderTexture = new RenderTexture(512, 512, 16);
                renderTexture.Create();

                // RenderTexture'� kameran�n targetTexture'� olarak at�yoruz
                cam.targetTexture = renderTexture;

                // RenderTexture'� "Assets/Textures" klas�r�ne kaydediyoruz
                SaveRenderTextureAsAsset(renderTexture, cam.gameObject.name);

                Debug.Log($"Created and assigned RenderTexture to {cam.name} for {cam.gameObject.name}");
            }
        }
    }

    private void SaveRenderTextureAsAsset(RenderTexture renderTexture, string mirrorName)
    {
        // Assets/Textures klas�r�n�n var olup olmad���n� kontrol ediyoruz, yoksa olu�turuyoruz
        string texturesFolderPath = "Assets/Textures";
        if (!Directory.Exists(texturesFolderPath))
        {
            Directory.CreateDirectory(texturesFolderPath);
            AssetDatabase.Refresh(); // Yeni klas�r olu�turuldu�u i�in AssetDatabase'i yenilemek gerekiyor
        }

        // RenderTexture'� asset olarak kaydetmek i�in yol belirleniyor
        string path = $"{texturesFolderPath}/{mirrorName}_RenderTexture.renderTexture";

        // RenderTexture'� asset olarak kaydediyoruz
        AssetDatabase.CreateAsset(renderTexture, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Saved RenderTexture as asset at {path}");
    }
}
