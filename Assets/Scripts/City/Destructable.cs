using UnityEngine;

public class Destructable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Player")) return;
        
        Destroy(gameObject);
    }
}
