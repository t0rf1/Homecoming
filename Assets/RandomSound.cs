using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
   AudioSource audioSource;
   public List<AudioClip> clip;

    private void Start()
    {
       
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(playRandom());

    }

    IEnumerator playRandom()
    {
        while (true)
        {
            var randomValue = Random.Range(0, 10);
            if (randomValue == 9)
            {
                audioSource.PlayOneShot(clip[Random.Range(0,clip.Count)]);
            }
            yield return new WaitForSeconds(1);
        }
       
    }
}
