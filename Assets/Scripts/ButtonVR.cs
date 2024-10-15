using System.Collections;
using UnityEngine;

public class ButtonVR : MonoBehaviour
{
    public GameObject button;
    public GameObject targetObject; // Rotasyonunu de�i�tirmek istedi�iniz nesne
    public float moveSpeed = 1f; // Butonun hareket h�z�
    public float rotationSpeed = 1f; // Hedef nesnenin d�nd�rme h�z�
    private Vector3 initialPosition; // Butonun ba�lang�� pozisyonu
    private Vector3 pressedPosition = new Vector3(0, -0.004f, 0); // Butonun bas�ld���nda gidece�i pozisyon
    private bool isPressed;

    private void Start()
    {
        // Butonun ba�lang�� pozisyonunu kaydet
        initialPosition = button.transform.localPosition;
        isPressed = false;
    }

    // Butona bas�ld���nda tetiklenir
    public void OnSelectEntered()
    {
        if (!isPressed)
        {
            // Butonu basma pozisyonuna indir ve hedef nesnenin rotas�n� d�nd�r
            StartCoroutine(MoveButtonAndRotate());
            isPressed = true;
        }
    }

    // Butonu hedef pozisyona g�t�r�p, sonra geri d�nen coroutine
    private IEnumerator MoveButtonAndRotate()
    {
        // �lk olarak butonu a�a��ya indir (pressedPosition)
        yield return StartCoroutine(MoveButton(button.transform.localPosition, pressedPosition));

        // Hedef nesneyi yava��a d�nd�r
        yield return StartCoroutine(RotateObject(targetObject.transform.rotation, targetObject.transform.rotation * Quaternion.Euler(0, 45, 0)));

        // Buton hedefe ula�t�ktan sonra bir s�re bekleyebiliriz (iste�e ba�l�)
        yield return new WaitForSeconds(0.5f); // 0.5 saniye bekle

        // Ard�ndan butonu eski pozisyonuna geri ta��
        yield return StartCoroutine(MoveButton(pressedPosition, initialPosition));

        isPressed = false; // Buton hareketi tamamland�ktan sonra tekrar bas�labilir duruma getir
    }

    // Butonu iki pozisyon aras�nda yava��a hareket ettiren coroutine
    private IEnumerator MoveButton(Vector3 startPos, Vector3 endPos)
    {
        float time = 0;
        while (time < 1)
        {
            // Lerp fonksiyonu ile iki pozisyon aras�nda yumu�ak ge�i� sa�lan�r
            button.transform.localPosition = Vector3.Lerp(startPos, endPos, time);
            time += Time.deltaTime * moveSpeed;
            yield return null; // Bir frame bekle
        }
        button.transform.localPosition = endPos; // Son pozisyonu garanti etmek i�in
    }

    // Hedef nesneyi yava��a d�nd�ren coroutine
    private IEnumerator RotateObject(Quaternion startRotation, Quaternion endRotation)
    {
        float time = 0;
        while (time < 1)
        {
            // Lerp fonksiyonu ile iki rotasyon aras�nda yumu�ak ge�i� sa�lan�r
            targetObject.transform.rotation = Quaternion.Lerp(startRotation, endRotation, time);
            time += Time.deltaTime * rotationSpeed;
            yield return null; // Bir frame bekle
        }
        targetObject.transform.rotation = endRotation; // Son pozisyonu garanti etmek i�in
    }
}
