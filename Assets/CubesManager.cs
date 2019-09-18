using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesManager : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private GameObject[] _cubesOnScene;
    
    private const float _SPAWN_VARIANCE = 25;
    private const float _LIFETIME_MIN = 8.0f;
    private const float _LIFETIME_MAX = 12.0f;
    private const int _MAX_CUBE_COUNT = 2500;

    public int count;

    private void Awake()
    {
        _cubesOnScene = new GameObject[_MAX_CUBE_COUNT];
        Init();
    }

    private void Init()
    {
        for (var i = 0; i < _cubesOnScene.Length; i++)
        {
            var newCube = SpawnCube();
            _cubesOnScene[i] = newCube;
            count++;
        }
    }
    
    private void Update()
    {
        var i = 0;
        for (i = 0; i < _cubesOnScene.Length; i++)
        {
            if (_cubesOnScene[i] == null)
            {
                var newCube = SpawnCube();
                _cubesOnScene[i] = newCube;
                count++;
                return;
            }
            
            var cube = _cubesOnScene[i].GetComponent<Cube>();
            if (cube.HasExpired)
            {
                Destroy(_cubesOnScene[i]);
                count--;
            }
        }
        
    }

    private GameObject SpawnCube()
    {
        var x = Random.Range(-_SPAWN_VARIANCE, _SPAWN_VARIANCE);
        var z = Random.Range(-_SPAWN_VARIANCE, _SPAWN_VARIANCE);
        var location = _spawnLocation.position + new Vector3(x, 0, z);
        var go = Instantiate(_cubePrefab, location, Quaternion.identity);
        go.AddComponent<Rigidbody>();
        var cubeController = go.AddComponent<Cube>();

        var t = Random.Range(_LIFETIME_MIN, _LIFETIME_MAX);
        cubeController.SetLifetime(t);

        return go;
    }
}