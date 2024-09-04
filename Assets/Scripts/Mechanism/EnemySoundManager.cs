using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    public AudioSource enemyAudio;

    public AudioClip fireAudio, deathAudio;
    // Start is called before the first frame update
    public void Start()
    {
        enemyAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Update()
    {

    }

    public void fire()
    {
        enemyAudio.PlayOneShot(fireAudio);
    }

    public void death()
    {
        enemyAudio.PlayOneShot(deathAudio);
    }
}
