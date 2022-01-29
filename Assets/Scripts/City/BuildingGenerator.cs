using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{

    [SerializeField] private GameObject _building = null;
    [SerializeField] private Renderer _renderer = null;
    public void GenerateBuilding()
    {
        int random = Random.Range(0, 100);

        bool isActive = random < 50;
        _building.SetActive(isActive);

        if (!isActive)
        {
            return;
        }
        
        _renderer.materials[0].color = Random.ColorHSV(0.45f, .55f, 0.1f, 0.4f, 0.1f, 0.4f);
        
        Color windowsColor = Random.ColorHSV(0f, 1f, 0.4f, 0.9f, 0.6f, 0.85f);
        _renderer.materials[1].color = windowsColor;
        _renderer.materials[1].SetColor("_EmissionColor", windowsColor);
    }
}
