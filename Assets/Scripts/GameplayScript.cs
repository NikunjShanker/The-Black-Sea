using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayScript : MonoBehaviour
{
    private AudioSource buttonPressSFX;
    private AudioSource buttonHoverSFX;

    private GameObject clipboard;

    private bool caseSolved;
    private bool clipboardOpen;
    private bool[][] clipboardChecks = new bool[3][] { new bool[4], new bool[4], new bool[4] };

    // 0 is empty, 1 is check, 2 is incorrect, 3 is correct
    public Sprite[] checkSprites;
    public Image[] buttonRenderers;
    // 0 is open, 1 is close, 2 is incorrect, 3 is correct
    public AudioClip[] clips;

    private Image checkStatusButton;
    private TextMeshProUGUI statusText;
    private AudioSource audioSource;

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

        clipboard = GameObject.Find("Screen");
        checkStatusButton = GameObject.Find("Check Button").GetComponent<Image>();
        statusText = GameObject.Find("Status Text").GetComponent<TextMeshProUGUI>();
        audioSource = this.GetComponent<AudioSource>();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                clipboardChecks[i][j] = false;
            }
        }

        checkStatusButton.sprite = checkSprites[2];
        statusText.text = "Case:\nUnsolved";
        caseSolved = false;

        closeClipboardButton();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (clipboardOpen)
            {
                closeClipboardButton();
            }
            else
            {
                SceneManager.LoadSceneAsync(0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            clipboardOpen = !clipboardOpen;
            updateClipboard();
        }
    }

    private void updateClipboard()
    {
        if (clipboardOpen)
        {
            clipboard.SetActive(true);
            audioSource.clip = clips[0];
            audioSource.Play();
        }
        else
        {
            clipboard.SetActive(false);
            audioSource.clip = clips[1];
            audioSource.Play();
        }
    }

    public void updateChecks(Button button)
    {
        if (!caseSolved)
        {
            int buttonNum = Int32.Parse(button.name.Substring(14));

            int trialIndex = buttonNum / 4;
            int choiceIndex = buttonNum - trialIndex * 4;

            for (int i = 0; i < 4; i++)
            {
                if (i == choiceIndex)
                {
                    clipboardChecks[trialIndex][i] = true;
                    buttonRenderers[trialIndex * 4 + i].sprite = checkSprites[1];
                }
                else
                {
                    clipboardChecks[trialIndex][i] = false;
                    buttonRenderers[trialIndex * 4 + i].sprite = checkSprites[0];
                }
            }
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

    public void checkChecksStatus()
    {
        if (!caseSolved)
        {
            if (clipboardChecks[0][1] == true && clipboardChecks[1][2] == true && clipboardChecks[2][0] == true)
            {
                buttonRenderers[1].sprite = checkSprites[3];
                buttonRenderers[6].sprite = checkSprites[3];
                buttonRenderers[8].sprite = checkSprites[3];

                checkStatusButton.sprite = checkSprites[3];
                statusText.text = "Case:\nSolved";
                caseSolved = true;

                audioSource.clip = clips[3];
                audioSource.Play();
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    buttonRenderers[i].sprite = checkSprites[0];
                }

                checkStatusButton.sprite = checkSprites[2];
                statusText.text = "Case:\nUnsolved";
                caseSolved = false;

                audioSource.clip = clips[2];
                audioSource.Play();
            }
        }
    }

    public void closeClipboardButton()
    {
        clipboardOpen = false;
        updateClipboard();
    }

    public void muteButton()
    {
        buttonPressSFX.Play();
        UniversalScript.instance.changeMuteStatus();
    }
}
