using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CityTime : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _panel;
    [SerializeField] private GameObject _continueGo;
    [SerializeField] private int _rageMuliplierRage = 2;  
    
    public float MinuteLength = 0.4f;
    
    private void Start()
    {
        _continueGo.SetActive(false);

        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        bool dayIsOver = false;
        int minutes = 0;
        int hours = 5;
        string AMPM = "PM";

        while (!dayIsOver)
        {
            string extrazero = "";
            if (minutes < 10) extrazero = "0";
            string time = hours + ":" + extrazero+minutes + " " + AMPM;
            _text.text = time;

            minutes++;
            if (minutes == 60)
            {
                minutes = 0;
                hours++;
            }
          
            if (hours == 11)
            {
                dayIsOver = true;
            }
            
            yield return new WaitForSeconds(MinuteLength);
        }
        
        _text.text = hours + ":" + "0"+minutes + " " + AMPM;;

        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        Color c = _panel.color;
        float alpha = c.a;
        float duration = 2f;

        AudioSource bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
        float initVol = bgm.volume;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime/duration;
            bgm.volume = (1f - alpha) * initVol;
            c.a = alpha;
            _panel.color = c;
            yield return new WaitForEndOfFrame();
        }

        int destroyedCount = FindObjectOfType<BuildingCounter>().BuildingDestroyed;
        int bonusMaxRage = destroyedCount * _rageMuliplierRage;
        Rage.MaxRage += bonusMaxRage;
        _continueGo.GetComponent<ContinueMenu>().SetText(destroyedCount, bonusMaxRage);
        FindObjectOfType<CityPlayerController>().gameObject.SetActive(false);
        FindObjectOfType<stepsSFX>().Stepping = false;
        _continueGo.SetActive(true);
    }
}
