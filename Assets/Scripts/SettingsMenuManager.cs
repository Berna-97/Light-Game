using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SettingsMenuManager : MonoBehaviour
{
    [Header("Audio")]
    public Slider masterVol, musicVol, sfxVol;
    public AudioMixer mainAudioMixer;

    [Header("Graphics")]
    [SerializeField] private TMP_Dropdown qualityDropdown;

    const string MASTER_KEY = "MasterVolume";
    const string MUSIC_KEY = "MusicVolume";
    const string SFX_KEY = "SfxVolume";

    [Header("Toggle Sound")]
    public AudioSource audioSource; // seu AudioSource
    public AudioClip clip;          // clip que quer tocar
    private bool isPlaying = false; // flag para toggle


    public GameObject canvas;
    public GameObject optionCanvas;
    public PauseScript pauseScript;

    void Awake()
    {
        // ===== AUDIO =====
        // Aplica os valores do mixer antes de qualquer som tocar para evitar solavanco
        

        // ===== QUALITY =====
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new List<string>(QualitySettings.names));

        int currentQuality = PlayerPrefs.GetInt("Quality", QualitySettings.GetQualityLevel());
        QualitySettings.SetQualityLevel(currentQuality, true);

        qualityDropdown.SetValueWithoutNotify(currentQuality);
        qualityDropdown.RefreshShownValue();

        qualityDropdown.onValueChanged.RemoveAllListeners();
        qualityDropdown.onValueChanged.AddListener(OnQualityChanged);
    }

    private void Start()
    {
        LoadAudioSettings();
    }

    // ---------- QUALITY ----------
    public void OnQualityChanged(int index)
    {
        Debug.Log("Mudou para qualidade: " + index);
        QualitySettings.SetQualityLevel(index, true);
        PlayerPrefs.SetInt("Quality", index);
        PlayerPrefs.Save();
    }

    // ---------- AUDIO ----------
    public void ChangeMasterVolume()
    {
        SetVolume(MASTER_KEY, "MasterVol", masterVol.value);
    }

    public void ChangeMusicVolume()
    {
        SetVolume(MUSIC_KEY, "MusicVol", musicVol.value);
    }

    public void ChangeSfxVolume()
    {
        SetVolume(SFX_KEY, "SfxVol", sfxVol.value);
    }

    void SetVolume(string prefKey, string mixerParam, float sliderValue)
    {
        // Limita entre -80 e 0 dB
        sliderValue = Mathf.Clamp(sliderValue, -80f, 0f);

        mainAudioMixer.SetFloat(mixerParam, sliderValue);
        PlayerPrefs.SetFloat(prefKey, sliderValue);
        PlayerPrefs.Save();
    }

    void LoadAudioSettings()
    {
        float master = Mathf.Clamp(PlayerPrefs.GetFloat(MASTER_KEY, 0f), -80f, 0f);
        float music = Mathf.Clamp(PlayerPrefs.GetFloat(MUSIC_KEY, 0f), -80f, 0f);
        float sfx = Mathf.Clamp(PlayerPrefs.GetFloat(SFX_KEY, 0f), -80f, 0f);

        masterVol.SetValueWithoutNotify(master);
        musicVol.SetValueWithoutNotify(music);
        sfxVol.SetValueWithoutNotify(sfx);

        // Aplica os valores suavemente para evitar solavancos
        mainAudioMixer.SetFloat("MasterVol", master);
        mainAudioMixer.SetFloat("MusicVol", music);
        mainAudioMixer.SetFloat("SfxVol", sfx);
    }

    // ---------- TOGGLE AUDIO ----------
    public void TogglePlay()
    {
        if (isPlaying)
        {
            audioSource.Stop();
            isPlaying = false;
        }
        else
        {
            audioSource.clip = clip;
            audioSource.Play();
            isPlaying = true;
        }
    }

    public void ReturnFromOptions()
    {
        canvas.SetActive(true);
        optionCanvas.SetActive(false);
        pauseScript.UnPause();
    }
}
