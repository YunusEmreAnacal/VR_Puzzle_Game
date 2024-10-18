using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class VRPlayerTrigger : MonoBehaviour
{
    public Transform spawnPoint; // Spawnlanacak nokta
    private XRController[] controllers; // VR kontrolleri
    private GameObject player; // Oyuncu referansý

    private void Start()
    {
        // Oyuncu nesnesini bul (Player tag'ine sahip)
        player = GameObject.FindGameObjectWithTag("Player");

        // Tüm XR Controller bileþenlerini bul
        controllers = FindObjectsOfType<XRController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Player tag'ine sahip objeler için
        {
            // Oyuncunun kontrol edilebilirliðini devre dýþý býrak
            DisableControls(true);

            // Oyuncuyu spawn noktasýna taþý
            StartCoroutine(TeleportPlayer(spawnPoint.position, spawnPoint.rotation));
        }
    }

    private IEnumerator TeleportPlayer(Vector3 position, Quaternion rotation)
    {
        // 0.1 saniye bekle
        yield return new WaitForSeconds(0.1f);

        // Oyuncunun konumunu ve rotasýný güncelle
        player.transform.position = position;
        player.transform.rotation = rotation;

        // Kontrolleri devre dýþý býrak ve oyunu yeniden baþlat
        StartCoroutine(DisableControlsAndRestart(10f));
    }

    private IEnumerator DisableControlsAndRestart(float seconds)
    {
        yield return new WaitForSeconds(seconds); // 10 saniye bekle

        // Oyunu yeniden baþlat
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void DisableControls(bool disable)
    {
        foreach (var controller in controllers)
        {
            controller.enableInputActions = !disable; // Kontrolleri devre dýþý býrak
        }
    }
}
