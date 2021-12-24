using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitFromUI : MonoBehaviour
{
    [SerializeField] private GameObject UI = null;
    [SerializeField] private GameObject LoadingScreen = null;
    [SerializeField] private GameObject sceneID = null;

    public void Quit()
    {
        UI.SetActive(false);
    }

    public void QuitToMenu()
    {
        sceneID.GetComponent<SceneLoading>().sceneID = 0;
        LoadingScreen.SetActive(true);
    }
}
