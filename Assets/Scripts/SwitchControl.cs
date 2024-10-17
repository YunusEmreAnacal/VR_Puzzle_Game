using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SwitchControl : MonoBehaviour
{
    
    public Animator blueSwitch;
    public Animator redSwitch;
    public Animator blackSwitch;
    public Animator yellowSwitch;
    public GameObject Lamps;
    public GameObject Laser;

    // Audio Clips
    public AudioClip switchPressSound;
    public AudioClip wrongOrderSound;

    public AudioSource audioSource; // Reference to the AudioSource

    private List<string> correctOrder = new List<string> { "Blue", "Red", "Black", "Yellow" };
    private List<string> playerOrder = new List<string>();

    // �alterlerin daha �nce bas�l�p bas�lmad���n� kontrol etmek i�in
    private bool bluePressed = false;
    private bool redPressed = false;
    private bool blackPressed = false;
    private bool yellowPressed = false;

    void Start()
    {
        

        // AudioSource yoksa hata mesaj� verin
        if (audioSource == null)
        {
            Debug.LogError("AudioSource bile�eni bulunamad�! L�tfen bu bile�eni GameObject'e ekleyin.");
        }
    }


    // XR Simple Interactable ile bu fonksiyonu �a��raca��z
    public void OnSwitchTouched(string switchName)
    {
        if (switchName == "Blue" && !bluePressed)
        {
            blueSwitch.SetTrigger("ToggleSwitch");
            PlaySound(switchPressSound);
            playerOrder.Add(switchName);
            bluePressed = true;
        }
        else if (switchName == "Red" && !redPressed)
        {
            redSwitch.SetTrigger("ToggleSwitch");
            PlaySound(switchPressSound);
            playerOrder.Add(switchName);
            redPressed = true;
        }
        else if (switchName == "Black" && !blackPressed)
        {
            blackSwitch.SetTrigger("ToggleSwitch");
            PlaySound(switchPressSound);
            playerOrder.Add(switchName);
            blackPressed = true;
        }
        else if (switchName == "Yellow" && !yellowPressed)
        {
            yellowSwitch.SetTrigger("ToggleSwitch");
            PlaySound(switchPressSound);
            playerOrder.Add(switchName);
            yellowPressed = true;
        }

        // Check if the order is wrong
        if (playerOrder.Count > correctOrder.Count || playerOrder[playerOrder.Count - 1] != correctOrder[playerOrder.Count - 1])
        {
            Debug.LogWarning("Yanl�� s�rada bas�ld�! S�f�rlan�yor...");
            PlaySound(wrongOrderSound); // Play wrong order sound
            ResetSwitches();
        }
        else if (playerOrder.Count == correctOrder.Count)
        {
            Debug.LogWarning("Do�ru s�ra, elektrik a��l�yor!");
            Lamps.SetActive(true);
            Laser.SetActive(true);
            // Perform the electricity activation process here
        }
    }

    // Function to play sound
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            Debug.Log("Ses �al�n�yor: " + clip.name);
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Ses �al�namad�, AudioClip veya AudioSource eksik!");
        }
    }

    // �alterleri s�f�rlama fonksiyonu
    private void ResetSwitches()
    {
        playerOrder.Clear();

        bluePressed = false;
        redPressed = false;
        blackPressed = false;
        yellowPressed = false;

        // �alter animasyonlar�n� s�f�rlama
        blueSwitch.SetTrigger("ResetSwitch");
        redSwitch.SetTrigger("ResetSwitch");
        blackSwitch.SetTrigger("ResetSwitch");
        yellowSwitch.SetTrigger("ResetSwitch");
    }

   
}
