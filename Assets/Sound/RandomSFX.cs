using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSFX : MonoBehaviour
{
    public float pitchDeviation = 0.1f;
    AudioSource[] sources;

    // Start is called before the first frame update
    void Start()
    {
        sources = GetComponents<AudioSource>();
    }

    
    public void TriggerRandomSound()
    {
        int pick = Random.Range(0, sources.Length);
        //Debug.Log(pick);
        AudioSource src = sources[pick];
        float pitch = Random.Range(1f - pitchDeviation, 1f + pitchDeviation);

        src.pitch = pitch;
        src.Play();
    }
}
