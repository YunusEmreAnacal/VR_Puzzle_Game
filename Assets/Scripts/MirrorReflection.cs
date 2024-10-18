using UnityEngine;

public class MirrorReflection : MonoBehaviour
{
    private Camera mirrorCamera;

    void Awake()
    {
        // Prefab içindeki Camera bileþeni
        mirrorCamera = GetComponentInChildren<Camera>();

        if (mirrorCamera != null)
        {
            // Yeni bir RenderTexture oluþtur
            RenderTexture newRenderTexture = new RenderTexture(4000, 4000, 16);
            newRenderTexture.Create();

            mirrorCamera.targetTexture = newRenderTexture;

            Debug.Log($"{gameObject.name} için yeni RenderTexture oluþturuldu ve atandý.");
        }
        else
        {
            Debug.LogError("Aynada kamera bulunamadý!");
        }
    }
}
