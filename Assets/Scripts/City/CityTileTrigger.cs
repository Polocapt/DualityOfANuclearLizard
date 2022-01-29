using System;
using UnityEngine;

public class CityTileTrigger : MonoBehaviour
{
    private float _i, _j;
    private Action<(float, float), Vector2> _callback;

    public void Init(float i, float j, Action<(float, float), Vector2> callback)
    {
        _i = i;
        _j = j;
        _callback = callback;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Player")) return;

        var velocity = other.gameObject.GetComponent<Rigidbody>().velocity;
        _callback((_i, _j), new Vector2(velocity.x, velocity.z));
    }
}
