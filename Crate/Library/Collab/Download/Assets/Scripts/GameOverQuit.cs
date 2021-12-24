using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverQuit : MonoBehaviour
{
    public GameObject LoadingScreen;
    public GameObject sceneID;

    public void OnClick()
    {
        sceneID.GetComponent<SceneLoading>().sceneID = 0;
        ObjectsSpawner.gameOver = false;
        LoadingScreen.SetActive(true);
    }
}
