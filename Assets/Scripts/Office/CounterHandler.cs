using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterHandler : MonoBehaviour
{
    public TMPro.TMP_Text CounterText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetCounter(int input)
    {
        CounterText.text = ""+input;
    }

    public void SetCounterPercent(int input)
    {
        CounterText.text = input + "%";
    }

    public void SetString(string input)
    {
        CounterText.text = input;
    }
}
