using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using QFSW.QC;

// To reference the GameManager, use GameManager.Instance.publicScriptName   VERY IMPORTANT
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(GameManager).ToString());
                    _instance = singleton.AddComponent<GameManager>();
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

    [Command("ReloadScene")]
    [CommandDescription("Reloads the current scene without async")]
    public void LegacyRestart() // Reloads current scene without level manager
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Switch to loading screen scene, then start timer before actual call to LoadManagerLocal
    [Command]
    [CommandDescription("Load a scene by index")]
    public void LoadScene(int sceneIndex)
    {
        // SceneManager.LoadScene("LoadingScreen");
        StartCoroutine(StartSceneLoadingCoroutine(sceneIndex));
    }

    private IEnumerator StartSceneLoadingCoroutine(int sceneIndex)
    {
        // yield return new WaitForSecondsRealtime((float)0.2); // To let the loading screen scene itself fully load
        yield return SceneManager.LoadSceneAsync("LoadingScreen");
        FindObjectOfType<LoadManagerLocal>().StartSceneLoading(sceneIndex); // Call the LoadManagerLocal to load the actual scene
    }

    [Command("QuitGame")]
    [CommandDescription("Quits the game")]
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
    }
}
