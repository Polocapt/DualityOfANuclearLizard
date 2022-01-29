using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public CounterHandler PaperPercent;
    public CounterHandler AnswerPhonePercent;
    public CounterHandler RageCounter;
    public CounterHandler TimeDisplay;
    public GameObject StampToContinuePrompt;
    public GameObject dark_panel;

    int rage = 0;
    
    public float MinuteLength = 0.4f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateTime());    
    }

    IEnumerator UpdateTime()
    {
        TimeDisplay.gameObject.SetActive(true);

        bool dayIsOver = false;
        int minutes = 0;
        int hours = 9;
        string AMPM = "AM";


        


        while (!dayIsOver)
        {
            string extrazero = "";
            if (minutes < 10) extrazero = "0";
            string time = hours + ":" + extrazero+minutes + " " + AMPM;
            TimeDisplay.SetString(time);

            // update time
            minutes++;
            if (minutes == 60)
            {
                minutes = 0;
                hours++;
            }
            if (hours == 12 && AMPM == "AM")
            {
                AMPM = "PM";
            }
            if(hours == 13)
            {
                hours = 1;
            }
            if (hours == 5 && AMPM == "PM")
            {
                dayIsOver = true;
            }

            // wait for tick and continue
            yield return new WaitForSeconds(MinuteLength);
        }

        // the day is over!
        TimeDisplay.SetString("5:00 PM");

        // fade in dark panel
        //Material mat = 
        Color c = dark_panel.GetComponent<UnityEngine.UI.Image>().color;

        float alpha = c.a;

        while (alpha < 1f)
        {
            alpha += 0.05f;
            c.a = alpha;
            dark_panel.GetComponent<UnityEngine.UI.Image>().color = c;
            //mat.color = c;

            yield return new WaitForSeconds(0.1f);
        }

        yield break;
    }

    public void StartSigningPaper()
    {
        PaperPercent.gameObject.SetActive(true);
        PaperPercent.SetCounterPercent(0);
    }

    public void UpdatePaperSigningProgress(float percent)
    {
        percent = Mathf.Min(percent, 100f);
        PaperPercent.SetCounterPercent(Mathf.RoundToInt(percent));

        if (percent == 100f) ShowStampPrompt();
    }

    void ShowStampPrompt()
    {
        StampToContinuePrompt.SetActive(true);
        PaperPercent.gameObject.SetActive(false);
    }

    public void StampTriggered()
    {
        StampToContinuePrompt.SetActive(false);

        rage++;
        RageCounter.SetCounter(rage);
    }
}
