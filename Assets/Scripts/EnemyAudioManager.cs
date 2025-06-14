using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip damage;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void DamageSound()
    {
        audioSource.PlayOneShot(damage);
    }
}
