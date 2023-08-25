using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public Slider slider;
    //public string sceneName;
    public Text loadText;
    
    int loadSceneIndex;
    static int nextScene;


    public void Start()
    {
        StartCoroutine(LoadAsynSceneCorutine());
    }

    public static void LoadScene(int loadSceneIndex)
    {
        nextScene = ++loadSceneIndex;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadAsynSceneCorutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;

            if(slider.value < 0.9f)
            {
                slider.value = Mathf.MoveTowards(slider.value, 0.9f, Time.deltaTime);
            }
            if (slider.value < 0.9f)
            {
                slider.value = Mathf.MoveTowards(slider.value, 0.9f, Time.deltaTime);
            }
            else if(operation.progress >= 0.9f)
            {
                slider.value = Mathf.MoveTowards(slider.value, 1f, Time.deltaTime);
            }
            if(slider.value >= 1f)
            {
                loadText.text = "Press SpaceBar";
            }

            if (Input.GetKeyDown(KeyCode.Space) && slider.value >= 1f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}
