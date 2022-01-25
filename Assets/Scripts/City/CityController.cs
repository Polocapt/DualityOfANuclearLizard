using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour
{
    [SerializeField] private Renderer _base;

    [SerializeField] private List<BuildingGenerator> _buildingGenerators = null;

    public Renderer GetBase()
    {
        return _base;
    }

    public void InitializeBuildings()
    {
        foreach (BuildingGenerator generator in _buildingGenerators)
        {
            generator.GenerateBuilding();
        }
    }
}
