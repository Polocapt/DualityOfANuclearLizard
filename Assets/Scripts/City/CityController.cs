using System;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour
{
    [SerializeField] private Renderer _base;
    [SerializeField] private List<BuildingGenerator> _buildingGenerators = null;
    [SerializeField] private CityTileTrigger _trigger;
    
    public Renderer GetBase()
    {
        return _base;
    }

    public void InitializeBuildings(float i, float j, Action<(float,float), Vector2> callback)
    {
        _trigger.Init(i, j, callback);
        foreach (BuildingGenerator generator in _buildingGenerators)
        {
            generator.GenerateBuilding();
        }
    }
}
