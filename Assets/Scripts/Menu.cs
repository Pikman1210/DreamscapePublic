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
