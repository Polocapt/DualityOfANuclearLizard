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
    // Start is called before the first frame update
    void Start()
    {
        sources = GetComponents<AudioSource>();
        StartCoroutine(StepSound());
    }

    // Update is called once per frame
    void Update()
    {
        if (TestMode)
        {
            Stepping = Input.GetMouseButton(0);
        }
    }

    IEnumerator StepSound()
    {
        while (!CancelStepping)
        {
            if (Stepping)
            {
                int pick = Random.Range(0, sources.Length);
                //Debug.Log(pick);
                AudioSource src = sources[pick];
                float pitch = Random.Range(1f - pitchDeviation, 1f + pitchDeviation);

                src.pitch = pitch;
                src.Play();

                
            }
            

            yield return new WaitForSeconds(StepRate);
        }

        yield break;
    }
}
