using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public CounterHandler PaperPercent;
    public CounterHandler AnswerPhonePercent;
    public CounterHandler TimeDisplay;
    public GameObject StampToContinuePrompt;
    public GameObject dark_panel;
    public PaperManager PM;
    public GameObject OutdoorLight;
    public GameObject DayIsOver;
    Vector3 LightPos;

    public bool dayIsOver = false;
    public float deltaLightPos = 0.03f;

    public int rage = 0;
    
    public float MinuteLength = 0.4f;
    
    void Start()
    {
        LightPos = OutdoorLight.transform.position;
        StartCoroutine(UpdateTime());    
    }

    IEnumerator UpdateTime()
    {
        TimeDisplay.gameObject.SetActive(true);

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

            LightPos.y -= deltaLightPos;
            OutdoorLight.transform.position = LightPos;

            // wait for tick and continue
            yield return new WaitForSeconds(MinuteLength);
        }

        // the day is over!
        //dayIsOver
        TimeDisplay.SetString("5:00 PM");
        
        if (PM.CurrentPaper != null)
        {
            GameObject DrawingArea = PM.CurrentPaper.GetComponentInChildren<Drawing>().gameObject;
            Destroy(DrawingArea);
        }
        
        GameObject.Find("granulator").GetComponent<granulator>().playing = false;
        PaperPercent.gameObject.SetActive(false);
        StampToContinuePrompt.gameObject.SetActive(false);
        PM.stopIt = true;

        // fade in dark panel
        dark_panel.SetActive(true);
        Color c = dark_panel.GetComponent<UnityEngine.UI.Image>().color;
        AudioSource bgm = GameObject.Find("sfx").GetComponent<AudioSource>();
        float alpha = c.a;

        while (alpha < 1f)
        {
            alpha += 0.05f;
            bgm.volume = 1f - alpha;
            c.a = alpha;
            dark_panel.GetComponent<UnityEngine.UI.Image>().color = c;
            
            yield return new WaitForSeconds(0.1f);
        }

        GameObject.Find("door_sfx").GetComponent<AudioSource>().Play();
        DayIsOver.SetActive(true);
        bgm.Stop();
        GameObject.Find("granulator").GetComponent<granulator>().playing = false;
        bool Continue = false;
        int maxWait = 2000;

        while (!Continue && maxWait>0)
        {

            if (Input.GetKey(KeyCode.Space))
            {
                Continue = true;
            }

            maxWait--;

            yield return new WaitForSeconds(0.05f);
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void StartSigningPaper()
    {
        if (!PM.stopIt)
        {
            PaperPercent.gameObject.SetActive(true);
            PaperPercent.SetCounterPercent(0);
        }
    }

    public void UpdatePaperSigningProgress(float percent)
    {
        percent = Mathf.Min(percent, 100f);
        PaperPercent.SetCounterPercent(Mathf.RoundToInt(percent));

        if (percent == 100f) ShowStampPrompt();
    }

    void ShowStampPrompt()
    {
        if (!PM.stopIt)
        {
            StampToContinuePrompt.SetActive(true);
            PaperPercent.gameObject.SetActive(false);
        }
        
    }

    public void StampTriggered()
    {

        StampToContinuePrompt.SetActive(false);

        rage++;
        
    }
}
