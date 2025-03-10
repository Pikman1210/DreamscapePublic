using UnityEngine;
using TMPro;
using UnityEngine.UI;
using QFSW.QC;

public class LevelDisplay : MonoBehaviour
{
    public GameObject levelDetailsPanel;
    public TMP_Text titleText;
    public TMP_Text subtitleText;
    public TMP_Text descriptionText;
    public SpriteRenderer spriteObject;
    public Button playButton;


    string levelName;
    string subtitle;
    string description;
    Sprite image;
    int levelIndex;
    bool playable;

    public void LoadLevelDetails(LevelObject levelObject)
    {
        levelName = levelObject.name;
        subtitle = levelObject.subtitle;
        description = levelObject.description;
        // image = levelObject.image;
        levelIndex = levelObject.levelIndex;
        // playable = levelObject.playable;

        titleText.text = levelName;
        subtitleText.text = subtitle;
        descriptionText.text = description;
        // spriteObject.sprite = image;
        // playButton.interactable = playable;
    }

    [Command("level-menu-fix")]
    public void ResetMenuValue()
    {
        GameManager.Instance.UpdateValueBool(0, false);
    }

    public void OnPlayButtonPressed()
    {
        GameManager.Instance.LoadScene(levelIndex);
    }

}
