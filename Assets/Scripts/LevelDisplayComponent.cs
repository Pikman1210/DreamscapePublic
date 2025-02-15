using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDisplayComponent : MonoBehaviour
{
    
    public LevelObject levelObject;

    public void SendLevelDetails(LevelObject levelObject)
    {
        FindAnyObjectByType<LevelDisplay>().LoadLevelDetails(levelObject);
    }
}
