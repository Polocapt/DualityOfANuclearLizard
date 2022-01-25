using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    [SerializeField] private Transform _parent = null;
    [SerializeField] private GameObject _cityTilePrefab = null;
    
    private const float X_SIZE = 10;
    private const float Y_SIZE = 10;

    private List<GameObject> instances = null;

    private void Start()
    {
        instances = new List<GameObject>();

        GenerateCity();
    }

    private void GenerateCity()
    {
        Vector3 localScale = _cityTilePrefab.GetComponent<CityController>().GetBase().bounds.size;
        for (float i = -X_SIZE/2; i < X_SIZE/2; i++)
        {
            for (float j = -Y_SIZE/2; j < Y_SIZE/2; j++)
            {
                GameObject instance = Instantiate(_cityTilePrefab, _parent);
                instance.transform.position = new Vector3(i * localScale.x, 0, j * localScale.z);
                
                instance.GetComponent<CityController>().InitializeBuildings();
                
                instances.Add(instance);
            }
        }
    }

    [ContextMenu("Randomize")]
    private void Randomize()
    {
        foreach (GameObject instance in instances)
        {
            Destroy(instance);
        }
        instances.Clear();

        GenerateCity();
    }
}
