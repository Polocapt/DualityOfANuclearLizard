using System;
using TMPro;
using UnityEngine;

public class CounterHandler : MonoBehaviour
{
    public TMP_Text CounterText;

    public void SetCounter(float input)
    {
        CounterText.text = ""+Math.Round(input);
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
