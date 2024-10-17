using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public Material material;
    LaserBeam beam;

    public AudioClip laserSound;  // Lazer sesi için AudioClip
    public AudioSource audioSource;  // AudioSource referansý

    void Start()
    {
        

        // Eðer audioSource atanmýþsa, lazer sesi loop olarak çalýnsýn
        if (audioSource != null && laserSound != null)
        {
            audioSource.clip = laserSound;
            audioSource.loop = true;  // Sesin sürekli tekrarlanmasýný saðlamak için loop ayarlýyoruz
            audioSource.Play();  // Lazer sesi çalmaya baþlar
        }
        else
        {
            Debug.LogWarning("AudioSource veya AudioClip eksik!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(GameObject.Find("Laser Beam"));
        beam = new LaserBeam(gameObject.transform.position, gameObject.transform.forward, material);
    }
}
