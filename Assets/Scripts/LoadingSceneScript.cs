using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneScript : MonoBehaviour
{
    public static int sceneNum;

    private void Start()
    {
        SceneManager.LoadSceneAsync(sceneNum);
    }
}
