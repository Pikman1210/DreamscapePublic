using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// To reference the LoadManager, use LoadManager.Instance.publicScriptName   VERY IMPORTANT
// LoadManagerGlobal.Instance.LoadScene(sceneID);
public class LoadManagerGlobal : MonoBehaviour
{
    // Singleton pattern
    private static LoadManagerGlobal _instance;

    public static LoadManagerGlobal Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LoadManagerGlobal>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(LoadManagerGlobal).ToString());
                    _instance = singleton.AddComponent<LoadManagerGlobal>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Switch to loading screen scene, then start timer before actual call to LoadManagerLocal
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadSceneAsync("LoadingScreen");
        StartCoroutine(StartSceneLoadingCoroutine(sceneIndex));
    }

    private IEnumerator StartSceneLoadingCoroutine(int sceneIndex)
    {
        yield return new WaitForSecondsRealtime((float)0.2); // To let the loading screen scene itself fully load
        FindObjectOfType<LoadManagerLocal>().StartSceneLoading(sceneIndex); // Call the LoadManagerLocal to load the actual scene
    }

}