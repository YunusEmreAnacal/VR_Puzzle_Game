using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public Material material;
    LaserBeam beam;

    public AudioClip laserSound;  // Lazer sesi i�in AudioClip
    public AudioSource audioSource;  

    void Start()
    {
 
        if (audioSource != null && laserSound != null)
        {
            audioSource.clip = laserSound;
            audioSource.loop = true;  // Sesin s�rekli tekrarlanmas�n� sa�lamak i�in loop ayarl�yoruz
            audioSource.Play();  // Lazer sesi �almaya ba�lar
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
