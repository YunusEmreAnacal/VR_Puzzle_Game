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
        // Sahnedeki tüm kameralarý buluyoruz
        Camera[] allCameras = FindObjectsOfType<Camera>();

        foreach (Camera cam in allCameras)
        {
            // Kameranýn baðlý olduðu objenin "Mirror" adýný taþýyýp taþýmadýðýný kontrol ediyoruz
            if (cam.gameObject.tag.Contains("Mirror"))
            {
                // Yeni bir RenderTexture oluþturuyoruz
                RenderTexture renderTexture = new RenderTexture(512, 512, 16);
                renderTexture.Create();

                // RenderTexture'ý kameranýn targetTexture'ý olarak atýyoruz
                cam.targetTexture = renderTexture;

                // RenderTexture'ý "Assets/Textures" klasörüne kaydediyoruz
                SaveRenderTextureAsAsset(renderTexture, cam.gameObject.name);

                Debug.Log($"Created and assigned RenderTexture to {cam.name} for {cam.gameObject.name}");
            }
        }
    }

    private void SaveRenderTextureAsAsset(RenderTexture renderTexture, string mirrorName)
    {
        // Assets/Textures klasörünün var olup olmadýðýný kontrol ediyoruz, yoksa oluþturuyoruz
        string texturesFolderPath = "Assets/Textures";
        if (!Directory.Exists(texturesFolderPath))
        {
            Directory.CreateDirectory(texturesFolderPath);
            AssetDatabase.Refresh(); // Yeni klasör oluþturulduðu için AssetDatabase'i yenilemek gerekiyor
        }

        // RenderTexture'ý asset olarak kaydetmek için yol belirleniyor
        string path = $"{texturesFolderPath}/{mirrorName}_RenderTexture.renderTexture";

        // RenderTexture'ý asset olarak kaydediyoruz
        AssetDatabase.CreateAsset(renderTexture, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Saved RenderTexture as asset at {path}");
    }
}
