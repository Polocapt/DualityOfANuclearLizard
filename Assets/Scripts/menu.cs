using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public GameObject HowToPlay;
    public GameObject HowToPart1;
    public GameObject HowToPart2;
    bool page1 = true;
    bool HowToOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
