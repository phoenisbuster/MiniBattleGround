using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharProfileUI : MonoBehaviour
{
    public TextMeshProUGUI _name;
    public TextMeshProUGUI stamina;
    public TextMeshProUGUI time;
    public TextMeshProUGUI generation;
    public TextMeshProUGUI buiding;
    public TextMeshProUGUI music;

    public TextMeshProUGUI currency01;
    public TextMeshProUGUI currency02;

    public void SetInfo(string name, string stamina, string time, string generation, string building, string music, string currency01, string currency02)
    {
        this._name.text = name;
        this.stamina.text = stamina;
        this.time.text = time;
        this.generation.text = generation;
        this.buiding.text = building;
        this.music.text = music;
        this.currency01.text = currency01;
        this.currency02.text = currency02;
    }
}
