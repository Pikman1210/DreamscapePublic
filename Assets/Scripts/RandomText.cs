using QFSW.QC;
using TMPro;
using UnityEngine;

public class RandomText : MonoBehaviour
{
    public TextObject textObject;
    private TMP_Text subtitle;

    /*
    private void Start()
    {
        subtitle = GetComponent<TMP_Text>();
        subtitle.text = textObject.strings[Random.Range(0, textObject.strings.Length)];
    } */

    [Command]
    public void RandomizeText()
    {
        subtitle = GetComponent<TMP_Text>();
        subtitle.text = textObject.strings[Random.Range(0, textObject.strings.Length)];
    }
}
