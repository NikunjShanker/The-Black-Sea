using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineTriggerScript : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !audioSource.isPlaying)
        {
            audioSource.Play();
            Destroy(this);
        }
    }
}
