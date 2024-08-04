using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroControllerScript : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(nextScene());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadingSceneScript.sceneNum = 4;
            SceneManager.LoadSceneAsync(3);
        }
    }

    IEnumerator nextScene()
    {
        LoadingSceneScript.sceneNum = 4;
        yield return new WaitForSeconds(23.5f);
        SceneManager.LoadSceneAsync(3);
    }
}
