using UnityEngine;


[CreateAssetMenu(fileName = "New Text List", menuName = "TextList")]
public class TextObject : ScriptableObject
{

    public string[] strings;
    public int[] weight;

}
