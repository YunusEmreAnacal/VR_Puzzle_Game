using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class VRPlayerTrigger : MonoBehaviour
{
    public Transform spawnPoint; // Spawnlanacak nokta
    private XRController[] controllers; // VR kontrolleri
    private GameObject player; // Oyuncu referans�

    private void Start()
    {
        // Oyuncu nesnesini bul (Player tag'ine sahip)
        player = GameObject.FindGameObjectWithTag("Player");

        // T�m XR Controller bile�enlerini bul
        controllers = FindObjectsOfType<XRController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Player tag'ine sahip objeler i�in
        {
            // Oyuncunun kontrol edilebilirli�ini devre d��� b�rak
            DisableControls(true);

            // Oyuncuyu spawn noktas�na ta��
            StartCoroutine(TeleportPlayer(spawnPoint.position, spawnPoint.rotation));
        }
    }

    private IEnumerator TeleportPlayer(Vector3 position, Quaternion rotation)
    {
        // 0.1 saniye bekle
        yield return new WaitForSeconds(0.1f);

        // Oyuncunun konumunu ve rotas�n� g�ncelle
        player.transform.position = position;
        player.transform.rotation = rotation;

        // Kontrolleri devre d��� b�rak ve oyunu yeniden ba�lat
        StartCoroutine(DisableControlsAndRestart(10f));
    }

    private IEnumerator DisableControlsAndRestart(float seconds)
    {
        yield return new WaitForSeconds(seconds); // 10 saniye bekle

        // Oyunu yeniden ba�lat
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void DisableControls(bool disable)
    {
        foreach (var controller in controllers)
        {
            controller.enableInputActions = !disable; // Kontrolleri devre d��� b�rak
        }
    }
}
