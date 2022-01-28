using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{

    [SerializeField] private GameObject _building = null;
    public void GenerateBuilding()
    {
        int random = Random.Range(0, 100);

        bool isActive = random < 50;
        _building.SetActive(isActive);

        if (!isActive)
        {
            return;
        }
        
        Renderer rend = _building.GetComponent<Renderer>();
        rend.material.color = Random.ColorHSV(0.5f, .55f, 0.1f, 0.5f, 0.1f, 0.4f);
    }
}
