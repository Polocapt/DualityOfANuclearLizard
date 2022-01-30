using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    [SerializeField] private Transform _parent = null;
    [SerializeField] private GameObject _cityTilePrefab = null;
    [SerializeField] private RandomSFX _destroySfx = null;
    
    private const float X_SIZE = 10;
    private const float Y_SIZE = 10;
    private const float OFFSET = 5;

    private Vector3 _localScale = Vector3.zero;

    private Dictionary<(float, float), CityController> _cityDict;

    private void Start()
    {
        _cityDict = new Dictionary<(float, float), CityController>();
        _localScale = _cityTilePrefab.GetComponent<CityController>().GetBase().bounds.size;

        GenerateCity();
    }

    private void GenerateCity()
    {
        for (float i = -X_SIZE/2; i < X_SIZE/2; i++)
        {
            for (float j = -Y_SIZE/2; j < Y_SIZE/2; j++)
            {
                InstantiateCity(i, j);
            }
        }
    }

    private void InstantiateCity(float i, float j)
    {
        GameObject instance = Instantiate(_cityTilePrefab, _parent);
        instance.transform.position = new Vector3(i * _localScale.x, 0, j * _localScale.z);
                
        var controller = instance.GetComponent<CityController>();
        controller.InitializeBuildings(i,j,TileEntered);
                
        _cityDict.Add((i,j), controller);
    }

    private void TileEntered((float x, float y) coord, Vector2 dir)
    {
        for (float i = coord.x - OFFSET; i < coord.x + OFFSET; i++)
        {
            for (float j = coord.y - OFFSET; j < coord.y + OFFSET; j++)
            {
                if (!_cityDict.ContainsKey((i, j)))
                {
                    InstantiateCity(i, j);
                }
            }
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Randomize")]
    private void Randomize()
    {
        foreach (KeyValuePair<(float, float), CityController> instance in _cityDict)
        {
            Destroy(instance.Value);
        }
        _cityDict.Clear();

        GenerateCity();
    }
#endif
}
