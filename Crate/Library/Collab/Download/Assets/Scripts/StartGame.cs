using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    //public GameObject LoadingScreen;
    //public GameObject sceneID;
    [SerializeField] private GameObject SelectLevelUI = null;

    public void OnClick()
    {
        //sceneID.GetComponent<SceneLoading>().sceneID = 1;
        //LoadingScreen.SetActive(true);
        SelectLevelUI.SetActive(true);
    }
}