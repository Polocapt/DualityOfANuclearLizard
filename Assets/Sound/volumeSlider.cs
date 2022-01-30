using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class volumeSlider : MonoBehaviour
{
    public AudioMixer MasterMixer;
    Slider VolumeSlider;

    static float level = 0f;
    static float value = 1f;
    // Start is called before the first frame update
    void Start()
    {
        VolumeSlider = GetComponent<Slider>();
        VolumeSlider.value = value;
    }
    
    public void UpdateMainVolume()
    {
        value = VolumeSlider.value;

        float min = -32f;
        level = min * (1f - VolumeSlider.value);

        // fixe
        if (level == min) level = -80f;


        MasterMixer.SetFloat("MainVolume", level);
    }
}
