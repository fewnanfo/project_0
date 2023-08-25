using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class LodingSceneController : MonoBehaviour
{

    private string loadSceneName;
    static string nextScene;
    

    [SerializeField]
    public Image progressBar;

    public void LoadScene(string sceneName)
    {
        //nextScene = sceneName;
        //SceneManager.LoadScene("LoadingScene");
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += onSceneLoded;
        loadSceneName = sceneName;
        StartCoroutine(LoadSeneProcesss());

    }

    private void onSceneLoded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == loadSceneName)
        {
            SceneManager.sceneLoaded -= onSceneLoded;
        }
    }
 

    IEnumerator LoadSeneProcesss()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            if (op.progress < 0.85f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
