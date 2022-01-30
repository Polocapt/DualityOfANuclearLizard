using UnityEngine;

public class Destructable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Player")) return;

        DestroyBuilding();
    }

    public void DestroyBuilding()
    {
        FindObjectOfType<RandomSFX>().TriggerRandomSound();
        FindObjectOfType<BuildingCounter>().AddBuildingCounter();
        
        Destroy(gameObject);
    }
}
