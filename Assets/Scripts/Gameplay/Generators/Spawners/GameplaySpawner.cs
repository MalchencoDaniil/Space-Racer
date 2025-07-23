using System.Collections.Generic;
using UnityEngine;

public class GameplaySpawner : Spawner
{
    private float _currentDistance = 0f;
    private int _activeBiomeIndex = 0;
    private float _nextBiomeSwitch = 0f;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private List<Biome> _biomes = new List<Biome>();
    [SerializeField] private float _distanceToNextBiome = 3000f;

    [Space(15)]
    [SerializeField] private Vector3 _spawnDirection = Vector3.right;
    private List<Floor> _spawnedTiles = new();
    [SerializeField] private int _spawnTileCount = 3;

    private Player _player;

    private float PlayerDistance => GameManager.Instance.Distance;

    public void Init(Player player)
    {
        _player = player;
    }

    private void Start()
    {
        _activeBiomeIndex = Random.Range(0, _biomes.Count);
        _nextBiomeSwitch = _distanceToNextBiome;

        for (int i = 0; i < StartFloorTileCount; i++)
        {
            if (StartFloors != null && StartFloors.Count > 0)
                Spawn(_spawnDirection, StartFloors[Random.Range(0, StartFloors.Count)]);
            else
                SpawnBiomeFloor();
        }
    }

    private void Update()
    {
        if (PlayerDistance >= _nextBiomeSwitch)
        {
            ChangeBiome();
            _nextBiomeSwitch += _distanceToNextBiome;
        }

        if (_player.transform.position.z > _currentDistance - TileLength * _spawnTileCount)
        {
            for (int i = 0; i < _spawnTileCount; i++)
                SpawnBiomeFloor();
        }

        if (_spawnedTiles.Count > 0 && _player.transform.position.z - _spawnedTiles[0].transform.position.z >= 300)
            DeleteFloorTile();
    }

    private void ChangeBiome()
    {
        int _newIndex = Random.Range(0, _biomes.Count);

        if (_biomes.Count > 1)
        {
            while (_newIndex == _activeBiomeIndex)
                _newIndex = Random.Range(0, _biomes.Count);
        }

        _activeBiomeIndex = _newIndex;

        _mainCamera.backgroundColor = _biomes[_activeBiomeIndex]._colorTheme;
        RenderSettings.fogColor = _biomes[_activeBiomeIndex]._colorTheme;

        Debug.Log($"Biome switched to: {_activeBiomeIndex}");
    }

    public void Despawn()
    {
        foreach (var tile in _spawnedTiles)
        {
            if (tile != null)
                Destroy(tile.gameObject);
        }

        _spawnedTiles.Clear();
    }

    private void DeleteFloorTile()
    {
        Destroy(_spawnedTiles[0].gameObject);
        _spawnedTiles.RemoveAt(0);
    }

    private void SpawnBiomeFloor()
    {
        var biomes = _biomes[_activeBiomeIndex].FloorTiles;

        if (biomes == null || biomes.Count == 0)
            return;

        var floor = biomes[Random.Range(0, biomes.Count)];

        Spawn(_spawnDirection, floor);
    }

    private void Spawn(Vector3 spawnDirection, Floor floorToSpawn)
    {
        var tile = Instantiate(floorToSpawn, transform.position + spawnDirection.normalized * _currentDistance, transform.rotation, transform);
        _spawnedTiles.Add(tile);
        _currentDistance += TileLength;
    }
}