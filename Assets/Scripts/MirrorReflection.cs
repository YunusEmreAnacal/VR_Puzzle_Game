using UnityEngine;

public class MirrorReflection : MonoBehaviour
{
    private Camera mirrorCamera;

    void Awake()
    {
        // Prefab i�indeki Camera bile�eni
        mirrorCamera = GetComponentInChildren<Camera>();

        if (mirrorCamera != null)
        {
            // Yeni bir RenderTexture olu�tur
            RenderTexture newRenderTexture = new RenderTexture(4000, 4000, 16);
            newRenderTexture.Create();

            mirrorCamera.targetTexture = newRenderTexture;

            Debug.Log($"{gameObject.name} i�in yeni RenderTexture olu�turuldu ve atand�.");
        }
        else
        {
            Debug.LogError("Aynada kamera bulunamad�!");
        }
    }
}
