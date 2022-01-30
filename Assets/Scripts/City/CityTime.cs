using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CityTime : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    
    public float MinuteLength = 0.4f;

    private void Start()
    {
        StartCoroutine(Time());
    }

    private IEnumerator Time()
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
          
            if (hours == 11 )
            {
                dayIsOver = true;
            }
            
            yield return new WaitForSeconds(MinuteLength);
        }

        SceneManager.LoadScene(0);
    }
}
