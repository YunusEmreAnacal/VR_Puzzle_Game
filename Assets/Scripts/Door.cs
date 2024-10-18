using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject leftDoorParent;  // Sol kap�n�n parent objesi
    public GameObject rightDoorParent; // Sa� kap�n�n parent objesi
    public float rotationSpeed = 2f;   // D�nd�rme h�z�
    public AudioClip doorOpenSound;    // Kap� a��lma sesi
    private AudioSource audioSource;

    private bool isDoorOpened = false; // Kap� daha �nce a��ld� m�?

    void Start()
    {
        
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = doorOpenSound;
    }

    public void OpenDoor()
    {
        // Kap� daha �nce a��ld�ysa bu fonksiyondan ��k
        if (isDoorOpened) return;

        // Kap� a��lma sesini �al
        audioSource.Play();

        // Sol kap�y� 90 derece sola, sa� kap�y� 90 derece sa�a d�nd�r
        StartCoroutine(RotateDoor(leftDoorParent.transform, Vector3.up * 90));  // Sola d�nd�r
        StartCoroutine(RotateDoor(rightDoorParent.transform, Vector3.up * -90));  // Sa�a d�nd�r

        // Kap� a��ld���n� i�aretle
        isDoorOpened = true;
    }

    // Kap�y� yava��a d�nd�ren Coroutine
    IEnumerator RotateDoor(Transform doorParent, Vector3 targetRotation)
    {
        Quaternion startRotation = doorParent.rotation;
        Quaternion endRotation = Quaternion.Euler(doorParent.eulerAngles + targetRotation);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * rotationSpeed;
            doorParent.rotation = Quaternion.Slerp(startRotation, endRotation, t); // Yumu�ak ge�i�
            yield return null;  // Bir sonraki frame'i bekle
        }

        // Tam rotasyona ula��ld���nda kap�y� tam hedef a��s�na ayarla
        doorParent.rotation = endRotation;
    }
}
