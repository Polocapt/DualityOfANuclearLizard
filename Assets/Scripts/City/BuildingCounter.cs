using TMPro;
using UnityEngine;

public class BuildingCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private int _buildingDestroyed = 0;

    private void Start()
    {
        SetText();
    }

    public void AddBuildingCounter()
    {
        _buildingDestroyed++;
        SetText();
    }

    private void SetText()
    {
        _text.text = "Buildings Destroyed: " + _buildingDestroyed;
    }
}
