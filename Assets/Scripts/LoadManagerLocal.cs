using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadManagerLocal : MonoBehaviour
{
    // Temp loading screen
    public GameObject loadingScreen;
    public Slider slider;
    public TMP_Text loadingText;

    // Scene loading methods
    public void StartSceneLoading(int sceneIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex)); // Takes scene index as parameter and then starts Coroutine to load scene with loading screen
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneIndex);

        // loadingScreen.SetActive(true);
        // Unneeded when dedicated scene

        while (!loadOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);

            slider.value = progress;
            loadingText.text = progress * 100f + "%";

            yield return null;
        }
    }

}