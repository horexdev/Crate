using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [Header("Музыка и Звуки")]
    [SerializeField] private Sprite sprite_On = null;
    [SerializeField] private Sprite sprite_Off = null;
    [SerializeField] private Sprite Joystick_On = null;
    [SerializeField] private Sprite Buttons_On = null;

    [SerializeField] private Button music_btn = null;
    [SerializeField] private Button sounds_btn = null;
    [SerializeField] private Button SwitchControlsBtn = null;

    [SerializeField] private AudioSource Music = null;

    public bool music_OnOff = true;
    public bool sounds_OnOff = true;
    public bool Control; // True - Buttons | False - Joystick

    private void Start()
    {
        int temp_Music = PlayerPrefs.GetInt("MusicMute");
        int temp_Sounds = PlayerPrefs.GetInt("SoundsMute");
        int temp_ActiveControl = PlayerPrefs.GetInt("ActiveControl");

        if (temp_Music == 0)
        {
            music_btn.image.overrideSprite = sprite_On;
            music_OnOff = true;
        }
        else
        {
            music_btn.image.overrideSprite = sprite_Off;
            music_OnOff = false;
        }

        if (temp_Sounds == 0)
        {
            sounds_btn.image.overrideSprite = sprite_On;
            sounds_OnOff = true;
        }
        else
        {
            sounds_btn.image.overrideSprite = sprite_Off;
            sounds_OnOff = false;
        }

        if (temp_ActiveControl == 0)
        {
            SwitchControlsBtn.image.overrideSprite = Joystick_On;
            Control = false;
        }
        else
        {
            SwitchControlsBtn.image.overrideSprite = Buttons_On;
            Control = true;
        }
    }

    public void MuteMusic()
    {
        if (music_OnOff)
        {
            music_btn.image.overrideSprite = sprite_Off;
            music_OnOff = false;
            Music.mute = true;
        }
        else
        {
            music_btn.image.overrideSprite = sprite_On;
            music_OnOff = true;
            Music.mute = false;
        }

        PlayerPrefs.SetInt("MusicMute", Music.mute ? 1 : 0);
    }

    public void MuteSounds()
    {
        if (sounds_OnOff)
        {
            sounds_btn.image.overrideSprite = sprite_Off;
            sounds_OnOff = false;
        }
        else
        {
            sounds_btn.image.overrideSprite = sprite_On;
            sounds_OnOff = true;
        }

        PlayerPrefs.SetInt("SoundsMute", sounds_OnOff ? 0 : 1);
    }

    public void SwitchControls()
    {
        if (Control)
        {
            SwitchControlsBtn.image.overrideSprite = Joystick_On;
            Control = false;
        }
        else
        {
            SwitchControlsBtn.image.overrideSprite = Buttons_On;
            Control = true;
        }

        PlayerPrefs.SetInt("ActiveControl", Control ? 1 : 0);
        Debug.Log(PlayerPrefs.GetInt("ActiveControl"));
    }
}