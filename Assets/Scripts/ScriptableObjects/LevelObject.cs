using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level", menuName = "LevelObject")]
public class LevelObject : ScriptableObject
{

    public new string name;
    public string subtitle;
    public string description;

    public Sprite image;

    public int levelIndex;
    public bool playable = true;

}
