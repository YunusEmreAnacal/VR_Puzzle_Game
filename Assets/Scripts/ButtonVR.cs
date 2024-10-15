using System.Collections;
using UnityEngine;

public class ButtonVR : MonoBehaviour
{
    public GameObject button;
    public GameObject targetObject; // Rotasyonunu deðiþtirmek istediðiniz nesne
    public float moveSpeed = 1f; // Butonun hareket hýzý
    public float rotationSpeed = 1f; // Hedef nesnenin döndürme hýzý
    private Vector3 initialPosition; // Butonun baþlangýç pozisyonu
    private Vector3 pressedPosition = new Vector3(0, -0.004f, 0); // Butonun basýldýðýnda gideceði pozisyon
    private bool isPressed;

    private void Start()
    {
        // Butonun baþlangýç pozisyonunu kaydet
        initialPosition = button.transform.localPosition;
        isPressed = false;
    }

    // Butona basýldýðýnda tetiklenir
    public void OnSelectEntered()
    {
        if (!isPressed)
        {
            // Butonu basma pozisyonuna indir ve hedef nesnenin rotasýný döndür
            StartCoroutine(MoveButtonAndRotate());
            isPressed = true;
        }
    }

    // Butonu hedef pozisyona götürüp, sonra geri dönen coroutine
    private IEnumerator MoveButtonAndRotate()
    {
        // Ýlk olarak butonu aþaðýya indir (pressedPosition)
        yield return StartCoroutine(MoveButton(button.transform.localPosition, pressedPosition));

        // Hedef nesneyi yavaþça döndür
        yield return StartCoroutine(RotateObject(targetObject.transform.rotation, targetObject.transform.rotation * Quaternion.Euler(0, 45, 0)));

        // Buton hedefe ulaþtýktan sonra bir süre bekleyebiliriz (isteðe baðlý)
        yield return new WaitForSeconds(0.5f); // 0.5 saniye bekle

        // Ardýndan butonu eski pozisyonuna geri taþý
        yield return StartCoroutine(MoveButton(pressedPosition, initialPosition));

        isPressed = false; // Buton hareketi tamamlandýktan sonra tekrar basýlabilir duruma getir
    }

    // Butonu iki pozisyon arasýnda yavaþça hareket ettiren coroutine
    private IEnumerator MoveButton(Vector3 startPos, Vector3 endPos)
    {
        float time = 0;
        while (time < 1)
        {
            // Lerp fonksiyonu ile iki pozisyon arasýnda yumuþak geçiþ saðlanýr
            button.transform.localPosition = Vector3.Lerp(startPos, endPos, time);
            time += Time.deltaTime * moveSpeed;
            yield return null; // Bir frame bekle
        }
        button.transform.localPosition = endPos; // Son pozisyonu garanti etmek için
    }

    // Hedef nesneyi yavaþça döndüren coroutine
    private IEnumerator RotateObject(Quaternion startRotation, Quaternion endRotation)
    {
        float time = 0;
        while (time < 1)
        {
            // Lerp fonksiyonu ile iki rotasyon arasýnda yumuþak geçiþ saðlanýr
            targetObject.transform.rotation = Quaternion.Lerp(startRotation, endRotation, time);
            time += Time.deltaTime * rotationSpeed;
            yield return null; // Bir frame bekle
        }
        targetObject.transform.rotation = endRotation; // Son pozisyonu garanti etmek için
    }
}
