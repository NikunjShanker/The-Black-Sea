using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsMenuScript : MonoBehaviour
{
    private AudioSource buttonPressSFX;
    private AudioSource buttonHoverSFX;

    private void Start()
    {
        foreach (AudioSource audiosrc in GameObject.Find("UI SFX").GetComponents<AudioSource>())
        {
            if (audiosrc.clip.name == "button press sfx")
            {
                buttonPressSFX = audiosrc;
            }
            else if (audiosrc.clip.name == "button hover sfx")
            {
                buttonHoverSFX = audiosrc;
            }
            else
            {
                Debug.Log("Audio Source Error");
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            backButton();
        }
    }

    public void buttonHover(Button button)
    {
        buttonHoverSFX.Play();
        RectTransform buttonTransform = button.gameObject.GetComponent<RectTransform>();
        buttonTransform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
    }

    public void buttonUnhover(Button button)
    {
        RectTransform buttonTransform = button.gameObject.GetComponent<RectTransform>();
        buttonTransform.localScale = new Vector3(1, 1, 1);
    }

    public void muteButton()
    {
        buttonPressSFX.Play();
        UniversalScript.instance.changeMuteStatus();
    }

    public void backButton()
    {
        buttonPressSFX.Play();
        LoadingSceneScript.sceneNum = 0;
        SceneManager.LoadSceneAsync(3);
    }
}
