using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip moveSound, fireSound, deathSound;

    private bool isMovingSoundPlaying = false;
    // Start is called before the first frame update
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void movement()
    {
        if (!isMovingSoundPlaying)
        {
            audioSource.clip = moveSound;
            audioSource.loop = true; // Set the sound to loop
            audioSource.Play();
            isMovingSoundPlaying = true;
        }
    }

    public void StopMovementSound()
    {
        if (isMovingSoundPlaying)
        {
            audioSource.Stop();
            isMovingSoundPlaying = false;
        }
    }

    public void fire()
    {
        audioSource.PlayOneShot(fireSound);
    }

    public void death() 
    {
        audioSource.PlayOneShot(deathSound);
    }
}
