using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsActionsHandler : MonoBehaviour
{
    [SerializeField] private GameObject LoadingScreen = null;
    [SerializeField] private GameObject sceneID = null;
    [SerializeField] private GameObject SettingsUI = null;
    [SerializeField] private GameObject ShopUI = null;

    public void Retry() // Button GameOver: Retry
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ObjectsSpawner.gameOver = false;
    }

    public void DoubleBonus() // Button GameOver: Double
    {
        //advertising launch
        Debug.Log("You got x2 bonus!");
    }

    public void GoToMenu() // Button GameOver: GoToMenu
    {
        sceneID.GetComponent<SceneLoading>().sceneID = 0;
        ObjectsSpawner.gameOver = false;
        LoadingScreen.SetActive(true);
    }

    public void OpenSettings() { // Button MainMenu : Settings
        SettingsUI.SetActive(true);
    }

    public void OpenShop() { // Button MainMenu : Shop
        ShopUI.SetActive(true);
    }

    public void NextLevel() { // Button LevelComplete : NextLevel
        sceneID.GetComponent<SceneLoading>().sceneID = SceneManager.GetActiveScene().buildIndex+1;
        ObjectsSpawner.gameOver = false;
        LoadingScreen.SetActive(true);
    }
}