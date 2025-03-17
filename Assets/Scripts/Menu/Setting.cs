using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Slider Sound;
    public Slider Music;
    public AudioClip[] MusicList;
    public AudioSource GameTheme;
    private static int i = 0;
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
        int old_index = i;
        i = Random.Range(0, MusicList.Length);
        if(i == old_index)
        {
            i = i+1 < MusicList.Length? i+1 : 0;
        }
        GameTheme.GetComponent<AudioSource>().clip = MusicList[i];
        GameTheme.GetComponent<AudioSource>().Play();
        Debug.Log("Change Music: " + i);
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
