using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class DoorScript : MonoBehaviour
{
    TextMeshProUGUI informationText;
    PlayableDirector openDoor;
    PlayableDirector closeDoor;
    Transform gasParticleSystems;

    AudioSource openDoorSFX;
    AudioSource closeDoorSFX;
    AudioSource gasSFX;

    private bool doorsOpen;

    private void Start()
    {
        informationText = GameObject.Find("Info Text").GetComponent<TextMeshProUGUI>();

        gasParticleSystems = GameObject.Find("Gas Particle Systems").GetComponent<Transform>();

        openDoor = GameObject.Find("Open Doors Cutscene").GetComponent<PlayableDirector>();
        closeDoor = GameObject.Find("Close Doors Cutscene").GetComponent<PlayableDirector>();

        openDoorSFX = openDoor.GetComponent<AudioSource>();
        closeDoorSFX = openDoor.GetComponent<AudioSource>();
        gasSFX = gasParticleSystems.GetComponent<AudioSource>();

        doorsOpen = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            pressX();
        }
    }

    private void pressX()
    {
        if (!doorsOpen)
        {
            StartCoroutine(openDoorCoroutine());

            informationText.text = "";
            doorsOpen = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!doorsOpen && other.tag == "Player")
        {
            informationText.text = "Press -X- to Open Door";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        informationText.text = "";

        if (doorsOpen && other.tag == "Player")
        {
            closeDoor.Play();
            closeDoorSFX.Play();
            doorsOpen = false;
        }
    }

    IEnumerator playGas(ParticleSystem gasPS)
    {
        yield return new WaitForSeconds(1f);
        gasPS.Play();
    }

    IEnumerator openDoorCoroutine()
    {
        openDoorSFX.Play();

        yield return new WaitForSeconds(0.8f);

        gasSFX.Play();
        openDoor.Play();

        foreach (Transform child in gasParticleSystems)
        {
            ParticleSystem gasPS = child.gameObject.GetComponent<ParticleSystem>();
            StartCoroutine(playGas(gasPS));
        }
    }
}
