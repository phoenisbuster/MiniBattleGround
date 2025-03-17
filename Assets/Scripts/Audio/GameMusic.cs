using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    public AudioClip[] Music;
    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().clip = Music[i];
        GetComponent<AudioSource>().Play();
        //Invoke("ChangeTheme", GetComponent<AudioSource>().clip.length);
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<AudioSource>().isPlaying == false && AudioListener.pause == false)
        {
            ChangeTheme();
        }
    }
    public void ChangeTheme()
    {
        GetComponent<AudioSource>().Stop();
        int old_index = i;
        i = Random.Range(0, Music.Length);
        if(i == old_index)
        {
            i = i+1 < Music.Length? i+1 : 0;
        }
        GetComponent<AudioSource>().clip = Music[i];
        GetComponent<AudioSource>().Play();
        Debug.Log("Change Music: " + i);
        //Invoke("ChangeTheme", GetComponent<AudioSource>().clip.length);
    }

    void OnEnable() 
    {
        NakamaConnect.EndGame += PlaySound;
        CharacterControls.FinishRun += PlayYeah;
        MapGen.startTime += PlayBegin;
    }
    void OnDisable() 
    {
        NakamaConnect.EndGame -= PlaySound;
        CharacterControls.FinishRun -= PlayYeah;
        MapGen.startTime -= PlayBegin;
    }
    public void PlaySound(bool value)
    {
        GetComponent<AudioSource>().Pause();
        if(value)
        {
            transform.GetChild(0).GetComponent<AudioSource>().Play();
        }
        else
        {
            transform.GetChild(1).GetComponent<AudioSource>().Play();
        }
        StartCoroutine(SetDelay());
    }
    public void PlayYeah(GameObject value)
    {
        transform.GetChild(2).GetComponent<AudioSource>().Play();
    }

    public void PlayBegin()
    {
        transform.GetChild(3).GetComponent<AudioSource>().Play();
        StartCoroutine(StopMusic(transform.GetChild(3).GetComponent<AudioSource>()));
    }

    IEnumerator SetDelay()
    {
        yield return new WaitForSeconds(5);
        GetComponent<AudioSource>().UnPause();
    }

    IEnumerator StopMusic(AudioSource audio)
    {
        yield return new WaitForSeconds(3f);
        audio.Stop();
    }
}
