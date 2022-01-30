using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] private GameObject _vfxPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Player")) return;

        DestroyBuilding();
    }

    public void DestroyBuilding()
    {
        FindObjectOfType<RandomSFX>().TriggerRandomSound();
        FindObjectOfType<BuildingCounter>().AddBuildingCounter();

        Instantiate(_vfxPrefab, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
}
