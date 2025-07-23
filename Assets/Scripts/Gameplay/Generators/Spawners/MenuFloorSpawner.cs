using System.Collections.Generic;
using UnityEngine;

public class MenuFloorSpawner : Spawner, ISpawner
{
    private float _currentDistance = 0;

    [SerializeField] private Vector3 _spawnDirection = Vector3.right;
    private List<Floor> _spawnedTiles = new();

    private void Start()
    {
        _currentDistance = TileLength * StartFloorTileCount;

        for (int i = 0; i < StartFloorTileCount; i++)
        {
            var tile = Instantiate(StartFloors[Random.Range(0, StartFloors.Count)], transform.position + _spawnDirection.normalized * _currentDistance, Quaternion.identity, transform);

            _spawnedTiles.Add(tile);

            _currentDistance -= TileLength;
        }
    }

    private void Update()
    {
        float _distance = Vector3.Distance(transform.position, _spawnedTiles[_spawnedTiles.Count - 1].transform.position);

        if (_distance >= _currentDistance)
        {
            _currentDistance = 0;
            Spawn(_spawnDirection);
            DeleteFloorTile();
        }
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

    public void Spawn(Vector3 spawnDirection)
    {
        var tile = Instantiate(StartFloors[Random.Range(0, StartFloors.Count)], transform.position + spawnDirection.normalized * _currentDistance, Quaternion.identity, transform);

        _spawnedTiles.Add(tile);

        _currentDistance += TileLength;
    }
}