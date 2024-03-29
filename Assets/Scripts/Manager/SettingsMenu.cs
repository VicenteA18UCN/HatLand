using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
public class SettingsMenu : MonoBehaviour
{
    public Slider volSlider;
    public AudioMixer audioMixer;
    private AudioManager audioManager;
    public float volumeDef;
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void OnEnable()
    {
        this.VolumeControl();
        this.audioManager = GetComponent<AudioManager>();
    }
    void Start()
    {
        this.ResolutionList();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume",Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volume",volume);
        this.volumeDef = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);
    }

    void ResolutionList()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        int currentResolutionIndex = 0;
        List<string> options = new List<string>();

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height +  " @ " + resolutions[i].refreshRate + "hz";
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }

        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    
    void VolumeControl()
    {
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume",0.75f);
            volSlider.value = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            volSlider.value = PlayerPrefs.GetFloat("volume");
            audioMixer.SetFloat("volume",Mathf.Log10(PlayerPrefs.GetFloat("volume") * 20));
        }
    }

    public void OnClickSound()
    {
        audioManager.PlaySound("Button Sound",0.5f); 
    }
}
