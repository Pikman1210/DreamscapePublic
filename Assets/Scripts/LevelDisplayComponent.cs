using UnityEngine;

public class LevelDisplayComponent : MonoBehaviour
{
    
    public LevelObject levelObject;
    public GameObject levelDisplayPanel;

    public void SendLevelDetails(LevelObject levelObject)
    {
        FindAnyObjectByType<LevelDisplay>().LoadLevelDetails(levelObject);
    }

    public void OnMouseDown()
    {
        if (!GameManager.Instance.levelSelectMenu && levelObject.playable)
        {
            GameManager.Instance.UpdateValueBool(0, true);
            levelDisplayPanel.SetActive(true);
            SendLevelDetails(levelObject);
        }
    }
}
