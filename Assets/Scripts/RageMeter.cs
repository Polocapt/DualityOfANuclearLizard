using UnityEngine;

public class RageMeter : MonoBehaviour
{
    public CounterHandler RageCounter;
    public RectTransform RagePointer;
    
    void Update()
    {
        RageCounter.SetCounter(Rage.value);
        RagePointer.eulerAngles = new Vector3(0f, 0f, 17f - 185f * Rage.value / Rage.MaxRage);
    }
}
