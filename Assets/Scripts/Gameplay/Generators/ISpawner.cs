using UnityEngine;

interface ISpawner
{
    void Spawn(Vector3 _spawnDirection);
    void Despawn();
}