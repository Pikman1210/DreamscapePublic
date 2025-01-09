using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour {

    public AudioMixer audioMixer;

    // UI Elements References

    public TMP_Dropdown resolutionDropdown;

    // Panels (Menu Screens)
    public GameObject WelcomePanelObject;
    public GameObject OptionsPanelObject;
    public GameObject SavePanelObject;
    public GameObject DevPanelObject;

    [SerializeField]
    private bool DevCodeSent = false;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRateRatio.numerator / resolutions[i].refreshRateRatio.denominator + "hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
    }

    // Settings Menu

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }

    public void setResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // Sava Data Menu
    /*
    public void LoadSaveData ()
    {
        string SecretBeaten = PlayerPrefs.GetString("SecretBeaten", "false");
        SecretBeatenText.text = SecretBeaten;

        int HighScore = PlayerPrefs.GetInt("Level", 0);
        HighScoreSaveText.text = HighScore.ToString();

        float EndlessHighscore = PlayerPrefs.GetFloat("EndlessHighscore", 0);
        EndlessScoreText.text = EndlessHighscore.ToString("0");
    }

    public void ResetSavaData ()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Level", 1);
    }

    public void EditSaveData (int Data)
    {
        switch (Data)
        {
            case 1:
                PlayerPrefs.SetString("SecretBeaten", "true");
                break;
            case 2:
                int HighScore = PlayerPrefs.GetInt("Level");
                if (HighScore < 5f)
                {
                    HighScore = HighScore + 1;
                    PlayerPrefs.SetInt("Level", HighScore);
                    break;
                }
                break;
            default:
                Debug.LogError("Debug menu's shit is fucked");
                break;
        }
    }*/

    // Dev tools
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Space) && DevCodeSent == false)
        {
            Debug.Log("Dev Code");
            DevCodeSent = true;
            WelcomePanelObject.SetActive(false);
            OptionsPanelObject.SetActive(false);
            SavePanelObject.SetActive(false);
            DevPanelObject.SetActive(true);
            Invoke("ResetDevCode", 1f);
        }
    }

    private void ResetDevCode()
    {
        DevCodeSent = false;
    }

    public void LoadCustomScene(string index)
    {
        SceneManager.LoadScene(index);
    }

}
