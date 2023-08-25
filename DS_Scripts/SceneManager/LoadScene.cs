using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LoadScene : MonoBehaviour
{
    LoadingSceneManager loadingSceneManager;
    int sceneIndex;

    private void Awake()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

    }

    public void SceneChanger()
    {
        LoadingSceneManager.LoadScene(sceneIndex);
    }
}
