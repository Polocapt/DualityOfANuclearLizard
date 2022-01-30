using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeIn : MonoBehaviour
{
    public int increments = 200;
    public float fadeLength = 30f;
    public float waitTime = 30f;
    float interval = 0f;
    float deltaVol = 0f;
    float initVolume = 0f;
    AudioSource src;

    // Start is called before the first frame update
    void Start()
    {
        float inc = increments;
        interval = fadeLength / inc;
        
        src = GetComponent<AudioSource>();
        initVolume = src.volume;
        src.volume = 0f;

        deltaVol = initVolume / inc;
        StartCoroutine(FadeMe());
    }

    IEnumerator FadeMe()
    {
        yield return new WaitForSeconds(waitTime);

        while (fadeLength > 0)
        {
            fadeLength -= interval;
            src.volume += deltaVol;
            yield return new WaitForSeconds(interval);
        }

        src.volume = initVolume;
        yield break;
    }
}
