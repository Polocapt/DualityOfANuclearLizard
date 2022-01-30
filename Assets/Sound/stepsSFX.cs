using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stepsSFX : MonoBehaviour
{
    public bool Stepping = false;
    public bool TestMode = false;
    public float StepRate = 0.6f;
    public float pitchDeviation = 0.1f;

    bool CancelStepping = false;

    AudioSource[] sources;
    void Start()
    {
        sources = GetComponents<AudioSource>();
        StartCoroutine(StepSound());
    }
    
    private IEnumerator StepSound()
    {
        while (!CancelStepping)
        {
            if (Stepping)
            {
                int pick = Random.Range(0, sources.Length);
                AudioSource src = sources[pick];
                
                float pitch = Random.Range(1f - pitchDeviation, 1f + pitchDeviation);
                src.pitch = pitch;
                src.Play();
            }
            
            yield return new WaitForSeconds(StepRate);
        }
    }
}
