using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class volumeSlider : MonoBehaviour
{
    public AudioMixer MasterMixer;
    Slider VolumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        VolumeSlider = GetComponent<Slider>();
    }
    
    public void UpdateMainVolume()
    {
        float level = 1f- VolumeSlider.value;
        level = - 80f* Mathf.Pow(level, 3);
        MasterMixer.SetFloat("MainVolume", level);
    }
}
