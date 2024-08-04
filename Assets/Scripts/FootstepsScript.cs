using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsScript : MonoBehaviour
{
    public static FootstepsScript instance;

    public AudioSource footstepSource;
    public AudioClip[] footstepClips;

    private void Start()
    {
        instance = this;
    }

    public void playFootstep()
    {
        if (!footstepSource.isPlaying)
        {
            int randomFootstep = Random.Range(0, 4);
            footstepSource.clip = footstepClips[randomFootstep];
            footstepSource.pitch = Random.Range(0.6f, 0.8f);
            footstepSource.Play();
        }
    }
}
