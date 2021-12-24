using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] private GameObject LoadingScreen = null;
    [SerializeField] private GameObject sceneID = null;

    public void LoadLevel(string Level)
    {
        sceneID.GetComponent<SceneLoading>().sceneName = Level;
        LoadingScreen.SetActive(true);
    }
}
