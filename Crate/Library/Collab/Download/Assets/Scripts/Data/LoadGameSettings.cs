using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameSettings : MonoBehaviour
{
    [SerializeField] private AudioSource music_source = null;

    private void Start()
    {
        if(!PlayerPrefs.HasKey("Lvl1"))
            PlayerPrefs.SetString("Lvl1", "unlocked");
        
        int temp = PlayerPrefs.GetInt("MusicMute");

        if (temp == 0)
        {
            music_source.mute = false;
        }
        else
        {
            music_source.mute = true;
        }

    }
}
