using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    [Header("Загружаемая сцена")]
    public int sceneID;
    public string sceneName;

    void Start()
    {
        StartCoroutine(AsyncLoad());
    }

    IEnumerator AsyncLoad()
    {
        if (sceneID == -1)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

            while (!operation.isDone)
            {
                //float progress = operation.progress / 0.9f;
                //LoadingImage.fillAmount = progress;
                yield return null;
            }
        }
        else
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

            while (!operation.isDone)
            {
                //float progress = operation.progress / 0.9f;
                //LoadingImage.fillAmount = progress;
                yield return null;
            }
        }
    }
}
