using UnityEngine;

public class MirrorReflection : MonoBehaviour
{
    private Camera mirrorCamera;

    void Awake()
    {
        // Prefab i�indeki Camera bile�enini buluyoruz
        mirrorCamera = GetComponentInChildren<Camera>();

        if (mirrorCamera != null)
        {
            // Yeni bir RenderTexture olu�turuyoruz (512x512 boyutunda, derinlik 16 bit)
            RenderTexture newRenderTexture = new RenderTexture(4000, 4000, 16);
            newRenderTexture.Create();

            // Kameran�n targetTexture'�na bu RenderTexture'� at�yoruz
            mirrorCamera.targetTexture = newRenderTexture;

            Debug.Log($"{gameObject.name} i�in yeni RenderTexture olu�turuldu ve atand�.");
        }
        else
        {
            Debug.LogError("Aynada kamera bulunamad�!");
        }
    }
}
