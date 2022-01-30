using TMPro;
using UnityEngine;

public class BuildingCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [HideInInspector] public int BuildingDestroyed = 0;

    private void Start()
    {
        SetText();
    }

    public void AddBuildingCounter()
    {
        BuildingDestroyed++;
        SetText();
    }
    
    private void SetText()
    {
        _text.text = "Buildings Destroyed: " + BuildingDestroyed;
    }
}
