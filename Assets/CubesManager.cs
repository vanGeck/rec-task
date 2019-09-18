using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubesManager : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private GameObject[] _cubesOnScene;
    [SerializeField] private Cube[] _cubeControllersOnScene;
    
    
    private const float _SPAWN_VARIANCE = 25;
    private const float _LIFETIME_MIN = 8.0f;
    private const float _LIFETIME_MAX = 12.0f;
    private const int _MAX_CUBE_COUNT = 2500;

    public int count;

    private void Awake()
    {
        _cubesOnScene = new GameObject[_MAX_CUBE_COUNT];
        _cubeControllersOnScene = new Cube[_MAX_CUBE_COUNT];
        Init();
    }

    private void Init()
    {
        for (var i = 0; i < _cubesOnScene.Length; i++)
        {
            var newCube = SpawnCube();
            _cubesOnScene[i] = newCube.Item1;
            _cubeControllersOnScene[i] = newCube.Item2;
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
                _cubesOnScene[i] = newCube.Item1;
                _cubeControllersOnScene[i] = newCube.Item2;
                count++;
                continue;
            }
            
            var cube = _cubesOnScene[i].GetComponent<Cube>();
            if (cube.HasExpired)
            {
                Destroy(_cubesOnScene[i]);
                count--;
            }
            else
            {
                cube.ManualUpdate();
            }
        }
        
        //CastRays();
        
    }

    private Tuple<GameObject, Cube> SpawnCube()
    {
        var x = Random.Range(-_SPAWN_VARIANCE, _SPAWN_VARIANCE);
        var z = Random.Range(-_SPAWN_VARIANCE, _SPAWN_VARIANCE);
        var location = _spawnLocation.position + new Vector3(x, 0, z);
        var go = Instantiate(_cubePrefab, location, Quaternion.identity);
        go.AddComponent<Rigidbody>();
        var cubeController = go.AddComponent<Cube>();

        var t = Random.Range(_LIFETIME_MIN, _LIFETIME_MAX);
        cubeController.SetLifetime(t);

        var tuple = new Tuple<GameObject, Cube>(go, cubeController);
        return tuple;
    }

    private void CastRays()
    {
        List<CubePosData> cubePositions = new List<CubePosData>();

        for (int i = 0; i < _cubesOnScene.Length; i++)
        {
            if (_cubesOnScene[i] == null) continue;
            
            var data = new CubePosData()
            {
                Pos = _cubesOnScene[i].transform.position,
                Id = i
            };
            
            cubePositions.Add(data);
        }

        var cubePositionsArray = cubePositions.ToArray(); 

        RaycastHit[] disposableBuffer = new RaycastHit[3];

        for (int i = 0; i < cubePositionsArray.Length; i++)
        {
            var hits = 0;
            if (Physics.RaycastNonAlloc(cubePositions[i].Pos, Vector3.forward, disposableBuffer) > 0) hits++;
            if (Physics.RaycastNonAlloc(cubePositions[i].Pos, Vector3.back, disposableBuffer) > 0) hits++;
            if (Physics.RaycastNonAlloc(cubePositions[i].Pos, Vector3.left, disposableBuffer) > 0) hits++;
            if (Physics.RaycastNonAlloc(cubePositions[i].Pos, Vector3.right, disposableBuffer) > 0) hits++;

            _cubeControllersOnScene[cubePositionsArray[i].Id].ReactToNeighbours(hits);
        }
    }

    private struct CubePosData
    {
        public Vector3 Pos;
        public int Id;
        public int Hits;
    }
    
}