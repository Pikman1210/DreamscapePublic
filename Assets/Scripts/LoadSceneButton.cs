using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(TryLoadScene);
    }

    private void TryLoadScene()
    {
        string sceneIDString = inputField.text;
        int sceneID;
        int.TryParse(sceneIDString, out sceneID);

        if (SceneExists(sceneID))
        {
            LoadManagerGlobal.Instance.LoadScene(sceneID); // Load the scene by calling the LoadManager
        }
        else
        {
            Debug.LogError("Scene with ID " + sceneID + " does not exist in build settings. Check for incorrect ID or incorrect build settings.");
        }
    }

    private bool SceneExists(int ID)
    {
        List<int> scenesInBuild = new List<int>();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            scenesInBuild.Add(SceneUtility.GetBuildIndexByScenePath(SceneUtility.GetScenePathByBuildIndex(i)));
        }
        return scenesInBuild.Any(t => t == ID);
    }
}