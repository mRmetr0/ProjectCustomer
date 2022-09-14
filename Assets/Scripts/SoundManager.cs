using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider masterVolume;
    [SerializeField] Slider musicVolume;
    [SerializeField] Slider sfxVolume;
    [Space]
    [SerializeField] AudioSource musicClip;
    [SerializeField] List<AudioSource> sfxClips;

    void Start()
    {
        musicClip.volume = musicVolume.value * masterVolume.value;
        musicClip.Play();

        if (!PlayerPrefs.HasKey("masterVolume"))
        {
            PlayerPrefs.SetFloat("masterVolume", 1);
            Load();
        } 
        else
        {
            Load();
        }

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }

        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    // Update is called once per frame
    void Update()
    {
        musicClip.volume = musicVolume.value * masterVolume.value;
        if(sfxClips != null)
        {
            for(int i = 0; i < sfxClips.Count; i++)
            {
                AudioSource sfxClip = sfxClips[i];
                sfxClip.volume = sfxVolume.value * masterVolume.value;
            }
        }

        Save();

        if (Input.GetKeyDown(KeyCode.P))
        {
            sfxClips[0].Play();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            sfxClips[1].Play();
        }
    }
    private void Load()
    {
        masterVolume.value = PlayerPrefs.GetFloat("masterVolume");
        musicVolume.value = PlayerPrefs.GetFloat("musicVolume");
        sfxVolume.value = PlayerPrefs.GetFloat("sfxVolume");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("masterVolume", masterVolume.value);
        PlayerPrefs.SetFloat("musicVolume", musicVolume.value);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume.value);
    }
}
