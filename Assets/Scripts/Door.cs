using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject leftDoorParent;  // Sol kapýnýn parent objesi
    public GameObject rightDoorParent; // Sað kapýnýn parent objesi
    public float rotationSpeed = 2f;   // Döndürme hýzý
    public AudioClip doorOpenSound;    // Kapý açýlma sesi
    private AudioSource audioSource;

    private bool isDoorOpened = false; // Kapý daha önce açýldý mý?

    void Start()
    {
        
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = doorOpenSound;
    }

    public void OpenDoor()
    {
        // Kapý daha önce açýldýysa bu fonksiyondan çýk
        if (isDoorOpened) return;

        // Kapý açýlma sesini çal
        audioSource.Play();

        // Sol kapýyý 90 derece sola, sað kapýyý 90 derece saða döndür
        StartCoroutine(RotateDoor(leftDoorParent.transform, Vector3.up * 90));  // Sola döndür
        StartCoroutine(RotateDoor(rightDoorParent.transform, Vector3.up * -90));  // Saða döndür

        // Kapý açýldýðýný iþaretle
        isDoorOpened = true;
    }

    // Kapýyý yavaþça döndüren Coroutine
    IEnumerator RotateDoor(Transform doorParent, Vector3 targetRotation)
    {
        Quaternion startRotation = doorParent.rotation;
        Quaternion endRotation = Quaternion.Euler(doorParent.eulerAngles + targetRotation);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * rotationSpeed;
            doorParent.rotation = Quaternion.Slerp(startRotation, endRotation, t); // Yumuþak geçiþ
            yield return null;  // Bir sonraki frame'i bekle
        }

        // Tam rotasyona ulaþýldýðýnda kapýyý tam hedef açýsýna ayarla
        doorParent.rotation = endRotation;
    }
}
