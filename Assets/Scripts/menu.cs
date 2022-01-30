using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    [SerializeField] private float _fadeDuration = 4f;
    [SerializeField] private float _holdDuration = 5f;
    public GameObject HowToPlay;
    public GameObject HowToPart1;
    public GameObject HowToPart2;
    bool page1 = true;
    bool HowToOpen = false;

    private Image _image;

    void Start()
    {
        _image = _background.GetComponent<Image>();
        StartCoroutine(SwitchBackground());
    }

    private IEnumerator SwitchBackground()
    {
        while (true)
        {
            yield return new WaitForSeconds(_holdDuration);
            Color c = _image.color;
            c.a = 1;
            float alpha = c.a;
            while (alpha > 0)
            {
                alpha -= Time.deltaTime/_fadeDuration;
                c.a = alpha;
                _image.color = c;
                yield return new WaitForEndOfFrame();
            }
            
            yield return new WaitForSeconds(_holdDuration);
            
            c.a = 0;
            alpha = c.a;
            while (alpha < 1f)
            {
                alpha += Time.deltaTime/_fadeDuration;
                c.a = alpha;
                _image.color = c;
                yield return new WaitForEndOfFrame();
            }
        }
    }
    
    void Update()
    {
        if (HowToOpen)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ToggleHowToPage();
            }
        }
        
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Rage.value = 0;
    }

    public void OpenHowTo()
    {
        HowToPlay.SetActive(true);
        HowToPart1.SetActive(true);
        HowToPart2.SetActive(false);
        page1 = true;
        HowToOpen = true;
    }

    public void CloseHowTo()
    {
        HowToPlay.SetActive(false);
        HowToOpen = false;
    }

    public void ToggleHowToPage()
    {
        if (page1)
        {
            HowToPart1.SetActive(false);
            HowToPart2.SetActive(true);
        }
        else
        {
            HowToPart1.SetActive(true);
            HowToPart2.SetActive(false);
        }

        page1 = !page1;
    }
}
