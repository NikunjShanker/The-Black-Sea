using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UniversalScript : MonoBehaviour
{
    public static UniversalScript instance;

    public Sprite[] muteSprites;

    public bool mute;
    private int currentSceneIndex;

    public void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        mute = false;
        currentSceneIndex = 0;

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (currentSceneIndex != SceneManager.GetActiveScene().buildIndex)
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            updateMuteButtonSprite();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            changeMuteStatus();
        }
    }

    public void changeMuteStatus()
    {
        mute = !mute;
        updateMuteButtonSprite();

        if (mute)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }

    private void updateMuteButtonSprite()
    {
        if (currentSceneIndex != 2 && currentSceneIndex != 3)
        {
            Image muteButtonRenderer = GameObject.Find("Mute Button").GetComponent<Image>();

            if (mute)
            {
                muteButtonRenderer.sprite = muteSprites[0];
            }
            else
            {
                muteButtonRenderer.sprite = muteSprites[1];
            }
        }

    }
}
