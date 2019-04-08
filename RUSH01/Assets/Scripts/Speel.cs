using System;
using UnityEngine;

[System.Serializable]
public class Speel {
    // actual Stat;
    public String name;
    public float domage;
    public float cool_down;
    public int lv;

    //next lv_stat
    public float dom_ratio;
    public float cool_down_reduction;

    //other
    public int lv_unlock;
    public string description;
    public Sprite Sprite_speel;

}