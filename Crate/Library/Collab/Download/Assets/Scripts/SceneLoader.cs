using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject settingsUI;

    public void SceneLoad()
    {
        if(gameObject.tag == "Retry")
        {
            Debug.Log("Retry!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            ObjectsSpawner.gameOver = false;
        }
    }
}
