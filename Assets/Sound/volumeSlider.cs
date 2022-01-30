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
        level = 1f- VolumeSlider.value;
        level = - 80f* Mathf.Pow(level, 3);
        MasterMixer.SetFloat("MainVolume", level);
    }
}
