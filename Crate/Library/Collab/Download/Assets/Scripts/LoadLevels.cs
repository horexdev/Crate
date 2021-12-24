using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevels : MonoBehaviour
{
    [SerializeField] private Sprite locked_lvl;

    private bool allLevelsUnlocked = false;

    private void Start()
    {
        if (!allLevelsUnlocked)
        {
            GetAllLevels();
        }
    }

    private void GetAllLevels()
    {
        foreach (Transform children in transform)
        {
            if (children.gameObject.name.Contains("Lvl"))
            {
                if (!PlayerPrefs.HasKey(children.gameObject.name))
                {
                    children.GetComponent<Image>().sprite = locked_lvl;
                    children.GetComponent<Button>().enabled = false;
                    children.GetComponentInChildren<Text>().enabled = false;
                }
            }
        }
    }
    
    public void unlockAllLevels()
    {
        allLevelsUnlocked = !allLevelsUnlocked;
        Debug.Log("Unlock: " + allLevelsUnlocked);
    }
}