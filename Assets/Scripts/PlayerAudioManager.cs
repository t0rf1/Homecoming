using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip damage;
    public AudioClip doDamage;
    public AudioClip death;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void DamageSound()
    {
        audioSource.PlayOneShot(damage);
    }
    public void DoDamageSound()
    {
        audioSource.PlayOneShot(doDamage);
    }

    public void DeathSound()
    {
        Debug.Log("dEATH SOUND PLAYED");
        audioSource.PlayOneShot(death);
    }
}
