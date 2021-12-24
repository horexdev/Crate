using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoShop : MonoBehaviour
{
    public GameObject LoadingScreen;
    public GameObject sceneID;

    public void OnClick()
    {
        sceneID.GetComponent<SceneLoading>().sceneID = 2;
        LoadingScreen.SetActive(true);
    }
}
