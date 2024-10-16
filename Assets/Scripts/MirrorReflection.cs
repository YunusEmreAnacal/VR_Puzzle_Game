using UnityEngine;

public class MirrorReflection : MonoBehaviour
{
    private Camera mirrorCamera;

    void Awake()
    {
        // Prefab içindeki Camera bileþenini buluyoruz
        mirrorCamera = GetComponentInChildren<Camera>();

        if (mirrorCamera != null)
        {
            // Yeni bir RenderTexture oluþturuyoruz (512x512 boyutunda, derinlik 16 bit)
            RenderTexture newRenderTexture = new RenderTexture(4000, 4000, 16);
            newRenderTexture.Create();

            // Kameranýn targetTexture'ýna bu RenderTexture'ý atýyoruz
            mirrorCamera.targetTexture = newRenderTexture;

            Debug.Log($"{gameObject.name} için yeni RenderTexture oluþturuldu ve atandý.");
        }
        else
        {
            Debug.LogError("Aynada kamera bulunamadý!");
        }
    }
}
