using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingInGame : MonoBehaviour
{
    public Slider Sound;
    public Slider Music;
    public AudioSource GameTheme;
    // Start is called before the first frame update
    void Start()
    {
        Sound.value = PlayerPrefs.GetFloat("Sound", 1);
        Music.value = PlayerPrefs.GetFloat("Music", 1);
    }
    public void AdjustMusic()
    {
        float VolumeValue = Music.value;
        GameTheme.GetComponent<AudioSource>().volume = VolumeValue;
        PlayerPrefs.SetFloat("Music", Music.value);
        //Debug.Log("Adjust Music");
    }

    public void ChangMusic()
    {
        GameTheme.GetComponent<GameMusic>().ChangeTheme();
        Debug.Log("Change Music");
        //PlayerPrefs.SetFloat("Music", Music.value);
    }
 
    public void AdjustVolume()
    {
        //Sound.value = PlayerPrefs.GetFloat("Sound", 1);
        float volume = Sound.value;
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Sound", Sound.value);
    }

    public void SaveSetting()
    {
        Sound.value = PlayerPrefs.GetFloat("Sound", 1);
        Music.value = PlayerPrefs.GetFloat("Music", 1);
    }

}
