using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using QFSW.QC;
using QFSW.QC.Actions;

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

    // Console command functions
    [Command("reload-scene-custom")]
    [CommandDescription("Reloads the current scene without async")]
    public void LegacyRestart() // Reloads current scene without level manager
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Switch to loading screen scene, then start timer before actual call to LoadManagerLocal
    [Command("load-scene-custom")]
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

    [Command("quit")]
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

    [Command("load-testing-scene")]
    [CommandDescription("Load the player testing scene")]
    public void LoadTestingScene()
    {
        LoadScene(3);
    }

    [Command("cursor")]
    [CommandDescription("Change cursor status. Visible, Lock, Confine, None.")]
    public void CursorStatus(string status)
    {
        if (status == "visible")
        {
            if (Cursor.visible == false)
            {
                Cursor.visible = true;
                Debug.Log("Cursor is now visible");
            }
            else if (Cursor.visible == true)
            {
                Cursor.visible = false;
                Debug.Log("Cursor is now invisible");
            } else
            {
                Debug.LogError("Error in cursor visibility");
            }
        } else if (status == "lock")
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Debug.Log("Cursor is now unlocked");
            }
            else if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Debug.Log("Cursor is now locked");
            } else
            {
                Debug.LogError("Error in cursor locking");
            }
        } else if (status == "confine")
        {
            if (Cursor.lockState == CursorLockMode.Confined)
            {
                Cursor.lockState = CursorLockMode.None;
                Debug.Log("Cursor is now unconfined");
            }
            else if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Debug.Log("Cursor is now confined");
            } else
            {
                Debug.LogError("Error in cursor confinement");
            }
        } else if (status == "none")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("Cursor is now visible and unlocked");
        } else
        {
            Debug.LogWarning("Invalid status.");
        }
    }

    [Command("list-scenes-in-build")]
    [CommandDescription("List all scenes in build settings")]
    public void ListAllScenesInBuild()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            Debug.Log("Scene " + i + ": " + SceneUtility.GetScenePathByBuildIndex(i));
        }
    }

    [Command("fog-enabled")]
    public void FogEnabled(bool status)
    {
        if (status == true)
        {
            RenderSettings.fog = true;
        } else if (status == false)
        {
            RenderSettings.fog = false;
        } else
        {
            return;
        }
    }
}
