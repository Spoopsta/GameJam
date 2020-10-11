using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

   // public Dropdown resolutionDropdown;
    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions =  Screen.resolutions;

        //clears all options from the drop down
        resolutionDropdown.ClearOptions();

        //this is a list, not an array, hence the different syntax. array has a fixed size, lists can be changed. 
        List<string> options = new List<string>();

        int currenResolutionIndex = 0;

        //loops through creating a list of options of resolutions
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currenResolutionIndex = i;
            }
        }

        //once loop is done it adds the ootions into the dropdown.
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currenResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //using the audio mixer to adjust the master volume. -80 -> 0
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    //changing quality levels. ITs just that easy wtf
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //setting the fullscreen.
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

}
