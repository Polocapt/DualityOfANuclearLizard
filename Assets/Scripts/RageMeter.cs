using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageMeter : MonoBehaviour
{
    public CounterHandler RageCounter;
    public RectTransform RagePointer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RageCounter.SetCounter(Rage.value);
        RagePointer.eulerAngles = new Vector3(0f, 0f, 17f - 185f * Rage.value / Rage.MaxRage);
    }
}
