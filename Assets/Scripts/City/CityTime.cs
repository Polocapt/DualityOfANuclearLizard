using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CityTime : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _panel;
    [SerializeField] private GameObject _continueGo;
    
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
            string time = "Time: " + hours + ":" + extrazero+minutes + " " + AMPM;
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
        
        _text.text = "Time: " + hours + ":" + "0"+minutes + " " + AMPM;;

        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        Color c = _panel.color;
        float alpha = c.a;
        float duration = 2f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime/duration;
            //bgm.volume = 1f - alpha;
            c.a = alpha;
            _panel.color = c;
            yield return new WaitForEndOfFrame();
        }

        _continueGo.GetComponent<ContinueMenu>().SetText(FindObjectOfType<BuildingCounter>().BuildingDestroyed);
        FindObjectOfType<CityPlayerController>().gameObject.SetActive(false);
        FindObjectOfType<stepsSFX>().Stepping = false;
        _continueGo.SetActive(true);
    }
}
