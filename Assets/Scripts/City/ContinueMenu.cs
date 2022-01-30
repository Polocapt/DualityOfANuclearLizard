using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueMenu : MonoBehaviour
{
    [SerializeField] private Button _nextDay;
    [SerializeField] private Button _mainMenu;
    [SerializeField] private TMP_Text _destroyedCount;
    [SerializeField] private TMP_Text _maxRage;

    private void Start()
    {
        _nextDay.onClick.AddListener(LoadNextDay);
        _mainMenu.onClick.AddListener(LoadMainMenu);
    }

    public void SetText(int counter, int maxRage)
    {
        _destroyedCount.text = "Buildings Destroyed: " + counter;
        _maxRage.text = "Max Rage +" + maxRage;
    }
    
    private void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextDay()
    {
        SceneManager.LoadScene(1);
    }
}
